library DllTestProject;

{ Important note about DLL memory management: ShareMem must be the
  first unit in your library's USES clause AND your project's (select
  Project-View Source) USES clause if your DLL exports any procedures or
  functions that pass strings as parameters or function results. This
  applies to all strings passed to and from your DLL--even those that
  are nested in records and classes. ShareMem is the interface unit to
  the BORLNDMM.DLL shared memory manager, which must be deployed along
  with your DLL. To avoid using BORLNDMM.DLL, pass string information
  using PChar or ShortString parameters. }

uses
  SysUtils,
  Classes,
  Windows;

type
  TDllRecordDCB = packed record
    DCBlength: DWORD;
    BaudRate: DWORD;
    Flags: Longint;
    wReserved: Word;
    XonLim: Word;
    XoffLim: Word;
    ByteSize: Byte;
    Parity: Byte;
    StopBits: Byte;
    XonChar: CHAR;
    XoffChar: CHAR;
    //ErrorChar: CHAR;
    EofChar: CHAR;
    EvtChar: CHAR;
    wReserved1: Word;
  end;

  TDllRecordKEY = packed record
    bKeyDown: BOOL;
    wRepeatCount: Word;
    wVirtualKeyCode: Word;
    wVirtualScanCode: Word;
    case Integer of
      0: (
        UnicodeChar: WCHAR;
        dwControlKeyState: DWORD);
      1: (
        AsciiChar: CHAR);
  end;

function DllMothod(var xDllRecord: TDllRecordKEY): Integer; stdcall;
var
  sl: TStringList;
begin
//xDllRecord.XonLim := 33;
//xDllRecord.XoffLim := 44;
//xDllRecord.ByteSize := 55;
//xDllRecord.Parity := 66;

  sl := TStringList.Create();
  try
{    sl.Add(IntToStr(SizeOf(xDllRecord)));
    sl.Add(IntToStr(xDllRecord.DCBlength));
    sl.Add(IntToStr(xDllRecord.BaudRate));
    sl.Add(IntToStr(xDllRecord.Flags));
    sl.Add(IntToStr(xDllRecord.wReserved));
    sl.Add(IntToStr(xDllRecord.XonLim));
    sl.Add(IntToStr(xDllRecord.XoffLim));
    sl.Add(IntToStr(xDllRecord.ByteSize));
    sl.Add(IntToStr(xDllRecord.Parity));
    sl.Add(IntToStr(xDllRecord.StopBits));
    sl.Add(IntToStr(Byte(xDllRecord.XonChar)));
    sl.Add(IntToStr(Byte(xDllRecord.XoffChar)));
//  sl.Add(IntToStr(Byte(xDllRecord.ErrorChar)));
    sl.Add(IntToStr(Byte(xDllRecord.EofChar)));
    sl.Add(IntToStr(Byte(xDllRecord.EvtChar)));
    sl.Add(IntToStr(xDllRecord.wReserved1));   }

    sl.Add(BoolToStr(xDllRecord.bKeyDown));
    sl.Add(IntToStr(xDllRecord.wRepeatCount));
    sl.Add(IntToStr(xDllRecord.wVirtualKeyCode));
    sl.Add(IntToStr(xDllRecord.wVirtualScanCode));
    sl.Add(xDllRecord.UnicodeChar);
    sl.Add(IntToStr(xDllRecord.dwControlKeyState));

    sl.SaveToFile('c:\a.txt');
  finally
    sl.Free;
  end;

  Result := SizeOf(xDllRecord);
end;

{$R *.res}

exports
   DllMothod;

begin
end.
