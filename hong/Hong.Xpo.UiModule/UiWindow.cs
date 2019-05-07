using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;
using DevExpress.Xpo;
using Hong.Xpo.Module;
using DevExpress.Xpo.Metadata;

namespace Hong.Xpo.UiModule
{
    public abstract class UiWindow
    {
        public UiWindow()
        {
            _views = new List<ViewerBase>();
        }

        private WindowStyle _windowStyle;
        public WindowStyle WindowStyle
        {
            get
            {
                return _windowStyle;
            }
            set
            {
                SetWindowStyle(value);
            }
        }

        private void SetWindowStyle(WindowStyle value)
        {
            if (_windowStyle != value)
            {
                _windowStyle = value;
                AutoLoad(XpobjectType, value, XpobjectManager);
            }
        }

        private Type _xpobjectType;
        public Type XpobjectType
        {
            get
            {
                return _xpobjectType;
            }
            set
            {
                SetXpobjectType(value);
            }
        }

        private void SetXpobjectType(Type value)
        {
            if (_xpobjectType != value)
            {
                SetXpobjectInfo(value);
                AutoLoad(value, WindowStyle, _xpobjectManager);
            }
        }

        private void SetXpobjectInfo(Type value)
        {
            _xpobjectType = value;
            _xpobjectManager = XpobjectCenter.Singleton.GetManager(value);
        }

        private XpobjectManager _xpobjectManager;
        public XpobjectManager XpobjectManager
        {
            get
            {
                return _xpobjectManager;
            }
        }

        private List<ViewerBase> _views;
        public List<ViewerBase> Views
        {
            get
            {
                return _views;
            }
        }

        private void ClereViewer()
        {
            Views.Clear();
        }

        #region AutoCreate

        private void AutoLoad(Type type, WindowStyle style, XpobjectManager manager)
        {
            if (type == null || manager == null)
            {
                return;
            }

            ClereViewer();

            switch (style)
            {
                case WindowStyle.Looking:
                    AutoViewerLooker(type);
                    break;

                case WindowStyle.Editing:
                    AutoViewerEditor(type, manager);
                    break;

                case WindowStyle.Simple:
                    AutoViewerLooker(type);
                    AutoViewerEditor(type, manager);
                    break;

                default:
                    break;
            }

            foreach (ViewerBase viewer in Views)
            {
                viewer.EventsLink();
                viewer.XpobjectManager = manager;
                AutoLayout(viewer, style);
            }
        }

        protected abstract void AutoLayout(ViewerBase viewer, WindowStyle style);

        private void AutoViewerEditor(Type type, XpobjectManager manager)
        {
            ViewerEditor editor = new ViewerEditor();

            UiContain contain;
            contain = ComponentManager.AutoCreateContain(SectionNameDefine.Contain_Editor_Main);
            contain.Viewer = editor;
            contain = ComponentManager.AutoCreateContain(SectionNameDefine.Contain_Editor_Action);
            contain.Viewer = editor;

            foreach (XpobjectFieldUIAttribute attribute in manager.XpobjectFieldUIAttributes)
            {
                if (attribute.Visible)
                {
                    UiControlObject control = ComponentManager.AutoCreateControl("Editor_" + attribute.FieldName, attribute.FieldType);
                    control.Viewer = editor;
                    control.PropertyName.Value = attribute.FieldName;
                    control.ParentName.Value = SectionNameDefine.Contain_Editor_Main;
                    control.Title.Value = attribute.FieldName;
                    control.Initialization(attribute.FieldType);
                }
            }

            UiButton button;
            button =  AutoControlButton(SectionNameDefine.Control_Button_Save, VariableValueDefine.Event_Save);
            button.Viewer = editor;
            button.ParentName.Value = SectionNameDefine.Contain_Editor_Action;
            button = AutoControlButton(SectionNameDefine.Control_Button_SaveAs, VariableValueDefine.Event_SaveAs);
            button.Viewer = editor;
            button.ParentName.Value = SectionNameDefine.Contain_Editor_Action;
            button = AutoControlButton(SectionNameDefine.Control_Button_Restore, VariableValueDefine.Event_Restore);
            button.Viewer = editor; ;
            button.ParentName.Value = SectionNameDefine.Contain_Editor_Action;

            AddViewer(editor);
        }

        private void AutoViewerLooker(Type type)
        {
            ViewerLooker looker = new ViewerLooker();

            UiContain contain;
            contain = ComponentManager.AutoCreateContain(SectionNameDefine.Contain_Looker_Main);
            contain.Viewer = looker;
            contain = ComponentManager.AutoCreateContain(SectionNameDefine.Contain_Looker_Action);
            contain.Viewer = looker;

            UiControlTable controlTable = ComponentManager.AutoCreateControlTable(SectionNameDefine.Control_Table);
            controlTable.Viewer = looker;
            controlTable.ParentName.Value = SectionNameDefine.Contain_Looker_Main;

            UiButton button;
            button = AutoControlButton(SectionNameDefine.Control_Button_Shutdown, VariableValueDefine.Event_Shutdown);
            button.Viewer = looker;
            button.ParentName.Value = SectionNameDefine.Contain_Looker_Action;
            button = AutoControlButton(SectionNameDefine.Control_Button_Refresh, VariableValueDefine.Event_Refresh);
            button.Viewer = looker;
            button.ParentName.Value = SectionNameDefine.Contain_Looker_Action;
            
            AddViewer(looker);
        }

        private UiButton AutoControlButton(string name, string eventName)
        {
            UiButton button = ComponentManager.AutoCreateButton(name);
            button.EventName.Value = eventName;
            button.Title.Value = eventName;
            return button;
        }

        #endregion 

        private void AddViewer(ViewerBase viewer)
        {
            if (viewer is ViewerLooker)
            {
                ViewerLooker looker = viewer as ViewerLooker;
                looker.SelectXpobjectChanged += new XpobjectChangedEventHandler(looker_SelectXpobjectChanged);
            }
            Views.Add(viewer);
        }

        private void RemoveViewer(ViewerBase viewer)
        {
            if (viewer is ViewerLooker)
            {
                ViewerLooker looker = viewer as ViewerLooker;
                looker.SelectXpobjectChanged -= new XpobjectChangedEventHandler(looker_SelectXpobjectChanged);
            }
            Views.Remove(viewer);
        }

        void looker_SelectXpobjectChanged(object sender, XpobjectChangedEventArgs e)
        {
            foreach (ViewerBase viewer in Views)
            {
                if (sender == viewer)
                {
                    continue;
                }
                viewer.CurrentXpobject = e.NewXpobject;
            }
        }


        #region LoadCreate

        public void Load(CxmlDocument[] cxmls)
        {
            //检验 Type一致性
            Type type = null;
            foreach (CxmlDocument cxml in cxmls)
            {
                if (type == null)
                {
                    type = cxml.GetXPObjectType();
                    continue;
                }
                if (type != cxml.GetXPObjectType())
                {
                    return;
                }
            }
            if (type == null)
            {
                return;
            }
            SetXpobjectInfo(type);
            foreach (CxmlDocument cxml in cxmls)
            {
                LoadViewer(cxml);
            }
            foreach (ViewerBase viewer in Views)
            {
                viewer.EventsLink();
                viewer.XpobjectManager = _xpobjectManager;
                LoadLayout(viewer);
            }
        }

        protected virtual void LoadLayout(ViewerBase viewer)
        {
        }

        private void LoadViewer(CxmlDocument cxml)
        {
            ViewerType viewerType = cxml.GetViewerStyle();
            ViewerBase viewer = null;
            if (viewerType == ViewerType.Editor)
            {
                viewer = new ViewerEditor();
            }
            else
            {
                viewer = new ViewerLooker();
            }
           
            ProfileBase profile = cxml.GetProfile();
            if (profile != null)
            {
                string[] cellerSections = profile.GetValues(SectionNameDefine.Cellers);
                foreach (string section in cellerSections)
                {
                    CellerBase celler = ComponentManager.CreateCeller(profile, section);
                    celler.Viewer = viewer;
                }

                foreach (CellerBase celler in viewer.Cellers)
                {
                    celler.Variables.LoadVariablesValue(profile);
                }
            }
            AddViewer(viewer);
        }
      
        #endregion
    }
}
