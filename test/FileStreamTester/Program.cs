using System;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;

public class BulkImageProcAsync
{
	public const String ImageBaseName = "tmpImage-";
	public const int numImages = 200;
	public const int numPixels = 512 * 512;

	// ProcessImage has a simple O(N) loop, and you can vary the number
	// of times you repeat that loop to make the application more CPU-
	// bound or more IO-bound.
	public static int processImageRepeats = 20;

	// Threads must decrement NumImagesToFinish, and protect
	// their access to it through a mutex.
	public static int NumImagesToFinish = numImages;
	public static Object[] NumImagesMutex = new Object[0];
	// WaitObject is signalled when all image processing is done.
	public static Object[] WaitObject = new Object[0];
	public class ImageStateObject
	{
		public byte[] pixels;
		public int imageNum;
		public FileStream fs;
	}

	[SecurityPermissionAttribute(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public static void MakeImageFiles()
	{
		int sides = (int)Math.Sqrt(numPixels);
		Console.Write("Making {0} {1}x{1} images... ", numImages,
			sides);
		byte[] pixels = new byte[numPixels];
		int i;
		for (i = 0; i < numPixels; i++)
			pixels[i] = (byte)i;
		FileStream fs;
		for (i = 0; i < numImages; i++)
		{
			fs = new FileStream(ImageBaseName + i + ".tmp",
				FileMode.Create, FileAccess.Write, FileShare.None,
				8192, false);
			fs.Write(pixels, 0, pixels.Length);
			FlushFileBuffers(fs.SafeFileHandle.DangerousGetHandle());
			fs.Close();
		}
		fs = null;
		Console.WriteLine("Done.");
	}

	public static void ReadInImageCallback(IAsyncResult asyncResult)
	{
		ImageStateObject state = (ImageStateObject)asyncResult.AsyncState;
		Stream stream = state.fs;
		int bytesRead = stream.EndRead(asyncResult);
		if (bytesRead != numPixels)
			throw new Exception(String.Format
				("In ReadInImageCallback, got the wrong number of " +
				"bytes from the image: {0}.", bytesRead));
		ProcessImage(state.pixels, state.imageNum);
		stream.Close();

		// Now write out the image.  
		// Using asynchronous I/O here appears not to be best practice.
		// It ends up swamping the threadpool, because the threadpool
		// threads are blocked on I/O requests that were just queued to
		// the threadpool. 
		FileStream fs = new FileStream(ImageBaseName + state.imageNum +
			".done", FileMode.Create, FileAccess.Write, FileShare.None,
			4096, false);
		fs.Write(state.pixels, 0, numPixels);
		fs.Close();

		// This application model uses too much memory.
		// Releasing memory as soon as possible is a good idea, 
		// especially global state.
		state.pixels = null;
		fs = null;
		// Record that an image is finished now.
		lock (NumImagesMutex)
		{
			NumImagesToFinish--;
			if (NumImagesToFinish == 0)
			{
				Monitor.Enter(WaitObject);
				Monitor.Pulse(WaitObject);
				Monitor.Exit(WaitObject);
			}
		}
	}

	public static void ProcessImage(byte[] pixels, int imageNum)
	{
		Console.WriteLine("ProcessImage {0}", imageNum);
		int y;
		// Perform some CPU-intensive operation on the image.
		for (int x = 0; x < processImageRepeats; x += 1)
			for (y = 0; y < numPixels; y += 1)
				pixels[y] += 1;
		Console.WriteLine("ProcessImage {0} done.", imageNum);
	}

	public static void ProcessImagesInBulk()
	{
		Console.WriteLine("Processing images...  ");
		long t0 = Environment.TickCount;
		NumImagesToFinish = numImages;
		AsyncCallback readImageCallback = new
			AsyncCallback(ReadInImageCallback);
		for (int i = 0; i < numImages; i++)
		{
			ImageStateObject state = new ImageStateObject();
			state.pixels = new byte[numPixels];
			state.imageNum = i;
			// Very large items are read only once, so you can make the 
			// buffer on the FileStream very small to save memory.
			FileStream fs = new FileStream(ImageBaseName + i + ".tmp",
				FileMode.Open, FileAccess.Read, FileShare.Read, 1, true);
			state.fs = fs;
			fs.BeginRead(state.pixels, 0, numPixels, readImageCallback,
				state);
		}

		// Determine whether all images are done being processed.  
		// If not, block until all are finished.
		bool mustBlock = false;
		lock (NumImagesMutex)
		{
			if (NumImagesToFinish > 0)
				mustBlock = true;
		}
		if (mustBlock)
		{
			Console.WriteLine("All worker threads are queued. " +
				" Blocking until they complete. numLeft: {0}",
				NumImagesToFinish);
			Monitor.Enter(WaitObject);
			Monitor.Wait(WaitObject);
			Monitor.Exit(WaitObject);
		}
		long t1 = Environment.TickCount;
		Console.WriteLine("Total time processing images: {0}ms",
			(t1 - t0));
	}

	public static void Cleanup()
	{
		for (int i = 0; i < numImages; i++)
		{
			File.Delete(ImageBaseName + i + ".tmp");
			File.Delete(ImageBaseName + i + ".done");
		}
	}

	public static void TryToClearDiskCache()
	{
		// Try to force all pending writes to disk, and clear the
		// disk cache of any data.
		byte[] bytes = new byte[100 * (1 << 20)];
		for (int i = 0; i < bytes.Length; i++)
			bytes[i] = 0;
		bytes = null;
		GC.Collect();
		Thread.Sleep(2000);
	}

	public static void Main(String[] args)
	{
		Console.WriteLine("Bulk image processing sample application," +
			" using asynchronous IO");
		Console.WriteLine("Simulates applying a simple " +
			"transformation to {0} \"images\"", numImages);
		Console.WriteLine("(Async FileStream & Threadpool benchmark)");
		Console.WriteLine("Warning - this test requires {0} " +
			"bytes of temporary space", (numPixels * numImages * 2));

		if (args.Length == 1)
		{
			processImageRepeats = Int32.Parse(args[0]);
			Console.WriteLine("ProcessImage inner loop - {0}.",
				processImageRepeats);
		}
		MakeImageFiles();
		TryToClearDiskCache();
		ProcessImagesInBulk();
		Cleanup();
	}
	[DllImport("KERNEL32", SetLastError = true)]
	private static extern void FlushFileBuffers(IntPtr handle);
}

