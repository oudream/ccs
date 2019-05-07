using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;

namespace Hong.Xpo.WebModule
{
    public class WebImage : WebControlEasy<System.Drawing.Image>
    {
        public WebImage()
        {
            _image = new Image();
            _fileUpload = new FileUpload();

            WebLayout layout = Layout as WebLayout;
            Label label = new Label();
            label.Text = @"<br/>";
            layout.TableCell.Controls.Add(_fileUpload);
            layout.TableCell.Controls.Add(label);
            layout.TableCell.Controls.Add(_image);
        }

        private Image _image;

        private FileUpload _fileUpload;

        protected override void LayoutChanged(WebLayout layout, Hong.Profile.Base.VariableListChangedArgs e)
        {
            _fileUpload.Width = layout.ComponentWidth;
            _image.Width = layout.ComponentWidth;
            _image.Height = Unit.Percentage(100);
        }

        protected override bool ComponentToValueImpl(out System.Drawing.Image value)
        {
            value = null;
            if (_fileUpload.PostedFile.ContentLength > 1024 * 100)
            {
                return false;
            }
            try
            {
                System.Drawing.Image _image = System.Drawing.Image.FromStream(_fileUpload.PostedFile.InputStream);
                if (_image != null)
                {
                    value = _image;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        protected override bool ValueToComponentImpl(System.Drawing.Image value)
        {
            if (Viewer.CurrentXpobject != null)
            {
                _image.ImageUrl = WebUrlDefine.ImageResponseUrl(Viewer.CurrentXpobject.GetType().FullName, Viewer.CurrentXpobject.Oid.ToString(), this.PropertyName.Value);
            }
            return false;
        }

        protected override void SetWebControlsID(string value)
        {
            _image.ID = "Image_" + value;
            _fileUpload.ID = "FileUpload_" + value;

            _fileUpload.Attributes.Clear();
            _fileUpload.Attributes.Add("onchange", String.Format("javascript:document.getElementById('{0}').src = document.getElementById('{1}').value", _image.ID, _fileUpload.ID));
        }

        protected override void TitleValueChanged(string value)
        {
            //nothing todo
        }
    }
}
