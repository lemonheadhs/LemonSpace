namespace System.Web.UI
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.ComponentModel.Design.Serialization;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Runtime;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security.Permissions;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Caching;
    using System.Web.Configuration;
    using System.Web.Hosting;
    using System.Web.Routing;
    using System.Web.UI.Adapters;
    using System.Web.UI.WebControls;
    using System.Web.Util;

    [DesignerSerializer("Microsoft.VisualStudio.Web.WebForms.ControlCodeDomSerializer, Microsoft.VisualStudio.Web, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.Serialization.CodeDomSerializer, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), ToolboxItem("System.Web.UI.Design.WebControlToolboxItem, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"), Themeable(false), ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Require), Bindable(true), DefaultProperty("ID"), DesignerCategory("Code"), Designer("System.Web.UI.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    public class Control : IComponent, IDisposable, IParserAccessor, IUrlResolutionService, IDataBindingsAccessor, IControlBuilderAccessor, IControlDesignerAccessor, IExpressionsAccessor
    {
        private string _cachedPredictableID;
        private string _cachedUniqueID;
        private ControlCollection _controls;
        private System.Web.UI.ControlState _controlState;
        private EventHandlerList _events;
        private string _id;
        private Control _namingContainer;
        private OccasionalFields _occasionalFields;
        internal System.Web.UI.Page _page;
        private Control _parent;
        private StateBag _viewState;
        private const int automaticIDCount = 0x80;
        private const string automaticIDPrefix = "ctl";
        private static readonly string[] automaticIDs = new string[] { 
            "ctl00", "ctl01", "ctl02", "ctl03", "ctl04", "ctl05", "ctl06", "ctl07", "ctl08", "ctl09", "ctl10", "ctl11", "ctl12", "ctl13", "ctl14", "ctl15", 
            "ctl16", "ctl17", "ctl18", "ctl19", "ctl20", "ctl21", "ctl22", "ctl23", "ctl24", "ctl25", "ctl26", "ctl27", "ctl28", "ctl29", "ctl30", "ctl31", 
            "ctl32", "ctl33", "ctl34", "ctl35", "ctl36", "ctl37", "ctl38", "ctl39", "ctl40", "ctl41", "ctl42", "ctl43", "ctl44", "ctl45", "ctl46", "ctl47", 
            "ctl48", "ctl49", "ctl50", "ctl51", "ctl52", "ctl53", "ctl54", "ctl55", "ctl56", "ctl57", "ctl58", "ctl59", "ctl60", "ctl61", "ctl62", "ctl63", 
            "ctl64", "ctl65", "ctl66", "ctl67", "ctl68", "ctl69", "ctl70", "ctl71", "ctl72", "ctl73", "ctl74", "ctl75", "ctl76", "ctl77", "ctl78", "ctl79", 
            "ctl80", "ctl81", "ctl82", "ctl83", "ctl84", "ctl85", "ctl86", "ctl87", "ctl88", "ctl89", "ctl90", "ctl91", "ctl92", "ctl93", "ctl94", "ctl95", 
            "ctl96", "ctl97", "ctl98", "ctl99", "ctl100", "ctl101", "ctl102", "ctl103", "ctl104", "ctl105", "ctl106", "ctl107", "ctl108", "ctl109", "ctl110", "ctl111", 
            "ctl112", "ctl113", "ctl114", "ctl115", "ctl116", "ctl117", "ctl118", "ctl119", "ctl120", "ctl121", "ctl122", "ctl123", "ctl124", "ctl125", "ctl126", "ctl127"
         };
        private const string automaticLegacyIDPrefix = "_ctl";
        private const int clientIDMode = 0x6000000;
        private const int clientIDModeOffset = 0x19;
        private const int controlAdapterResolved = 0x8000;
        private const int controlsCreated = 8;
        private const int controlStateApplied = 0x100000;
        private const int creatingControls = 0x100;
        private const int designMode = 0x10000;
        private const int designModeChecked = 0x20000;
        private const int disableChildControlState = 0x40000;
        private const int disableTheming = 0x1000;
        private const int disableViewState = 4;
        private const int effectiveClientIDMode = 0x18000000;
        private const int effectiveClientIDModeOffset = 0x1b;
        private const int enableThemingSet = 0x2000;
        internal const bool EnableViewStateDefault = true;
        internal static readonly object EventDataBinding = new object();
        private static readonly object EventDisposed = new object();
        internal static readonly object EventInit = new object();
        internal static readonly object EventLoad = new object();
        internal static readonly object EventPreRender = new object();
        internal static readonly object EventUnload = new object();
        internal SimpleBitVector32 flags;
        private const char ID_RENDER_SEPARATOR = '_';
        internal const char ID_SEPARATOR = '$';
        private const int idNotCalculated = 1;
        private const int idNotRequired = 0x40;
        private const int invisible = 0x10;
        private const int isNamingContainer = 0x80;
        internal const int isWebControlDisabled = 0x80000;
        internal const char LEGACY_ID_SEPARATOR = ':';
        private const int marked = 2;
        private const int mustRenderID = 0x800;
        private const int notVisibleOnPage = 0x200;
        private const int styleSheetApplied = 0x4000;
        private const int themeApplied = 0x400;
        private const int useGeneratedID = 0x200000;
        private const int validateRequestMode = 0x60000000;
        private const int validateRequestModeDirty = 0x400000;
        private const int validateRequestModeOffset = 0x1d;
        private const int viewStateMode = 0x1000000;
        private const int viewStateNotInherited = 0x800000;
        private const int visibleDirty = 0x20;

        [WebSysDescription("Control_OnDataBind"), WebCategory("Data")]
        public event EventHandler DataBinding
        {
            add
            {
                this.Events.AddHandler(EventDataBinding, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventDataBinding, value);
            }
        }

        [WebSysDescription("Control_OnDisposed")]
        public event EventHandler Disposed
        {
            add
            {
                this.Events.AddHandler(EventDisposed, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventDisposed, value);
            }
        }

        [WebSysDescription("Control_OnInit")]
        public event EventHandler Init
        {
            add
            {
                this.Events.AddHandler(EventInit, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventInit, value);
            }
        }

        [WebSysDescription("Control_OnLoad")]
        public event EventHandler Load
        {
            add
            {
                this.Events.AddHandler(EventLoad, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventLoad, value);
            }
        }

        [WebSysDescription("Control_OnPreRender")]
        public event EventHandler PreRender
        {
            add
            {
                this.Events.AddHandler(EventPreRender, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventPreRender, value);
            }
        }

        [WebSysDescription("Control_OnUnload")]
        public event EventHandler Unload
        {
            add
            {
                this.Events.AddHandler(EventUnload, value);
            }
            remove
            {
                this.Events.RemoveHandler(EventUnload, value);
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public Control()
        {
            if (this is INamingContainer)
            {
                this.flags.Set(0x80);
            }
        }

        protected internal virtual void AddedControl(Control control, int index)
        {
            if (control.OwnerControl != null)
            {
                throw new InvalidOperationException(System.Web.SR.GetString("Substitution_NotAllowed"));
            }
            if (control._parent != null)
            {
                control._parent.Controls.Remove(control);
            }
            control._parent = this;
            control._page = this.Page;
            control.flags.Clear(0x20000);
            Control namingContainer = this.flags[0x80] ? this : this._namingContainer;
            if (namingContainer != null)
            {
                control.UpdateNamingContainer(namingContainer);
                if ((control._id == null) && !control.flags[0x40])
                {
                    control.GenerateAutomaticID();
                }
                else if ((control._id != null) || (control._controls != null))
                {
                    namingContainer.DirtyNameTable();
                }
            }
            if (this._controlState >= System.Web.UI.ControlState.ChildrenInitialized)
            {
                control.InitRecursive(namingContainer);
                if (((control._controlState >= System.Web.UI.ControlState.Initialized) && (control.RareFields != null)) && control.RareFields.RequiredControlState)
                {
                    this.Page.RegisterRequiresControlState(control);
                }
                if (this._controlState >= System.Web.UI.ControlState.ViewStateLoaded)
                {
                    object savedState = null;
                    if ((this._occasionalFields != null) && (this._occasionalFields.ControlsViewState != null))
                    {
                        savedState = this._occasionalFields.ControlsViewState[index];
                        if (this.LoadViewStateByID)
                        {
                            control.EnsureID();
                            savedState = this._occasionalFields.ControlsViewState[control.ID];
                            this._occasionalFields.ControlsViewState.Remove(control.ID);
                        }
                        else
                        {
                            savedState = this._occasionalFields.ControlsViewState[index];
                            this._occasionalFields.ControlsViewState.Remove(index);
                        }
                    }
                    control.LoadViewStateRecursive(savedState);
                    if (this._controlState >= System.Web.UI.ControlState.Loaded)
                    {
                        control.LoadRecursive();
                        if (this._controlState >= System.Web.UI.ControlState.PreRendered)
                        {
                            control.PreRenderRecursiveInternal();
                        }
                    }
                }
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected virtual void AddParsedSubObject(object obj)
        {
            Control child = obj as Control;
            if (child != null)
            {
                this.Controls.Add(child);
            }
        }

        private void ApplySkin(System.Web.UI.Page page)
        {
            if (page == null)
            {
                throw new ArgumentNullException("page");
            }
            if (!this.flags[0x400] && ThemeableAttribute.IsTypeThemeable(base.GetType()))
            {
                page.ApplyControlSkin(this);
                this.flags.Set(0x400);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public virtual void ApplyStyleSheetSkin(System.Web.UI.Page page)
        {
            if (page != null)
            {
                if (this.flags[0x4000])
                {
                    throw new InvalidOperationException(System.Web.SR.GetString("StyleSheetAreadyAppliedOnControl"));
                }
                if (page.ApplyControlStyleSheet(this))
                {
                    this.flags.Set(0x4000);
                }
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void BeginRenderTracing(TextWriter writer, object traceObject)
        {
            RenderTraceListener.CurrentListeners.BeginRendering(writer, traceObject);
        }

        protected void BuildProfileTree(string parentId, bool calcViewState)
        {
            int num;
            calcViewState = calcViewState && !this.flags[4];
            if (calcViewState)
            {
                num = this.EstimateStateSize(this.SaveViewState());
            }
            else
            {
                num = 0;
            }
            int controlStateSize = 0;
            if (((this.Page != null) && (this.Page._registeredControlsRequiringControlState != null)) && this.Page._registeredControlsRequiringControlState.Contains(this))
            {
                controlStateSize = this.EstimateStateSize(this.SaveControlStateInternal());
            }
            this.Page.Trace.AddNewControl(this.UniqueID, parentId, base.GetType().FullName, num, controlStateSize);
            if (this._controls != null)
            {
                int count = this._controls.Count;
                for (int i = 0; i < count; i++)
                {
                    this._controls[i].BuildProfileTree(this.UniqueID, calcViewState);
                }
            }
        }

        internal bool CalculateEffectiveValidateRequest()
        {
            if (System.Web.Configuration.RuntimeConfig.GetConfig().HttpRuntime.RequestValidationMode >= VersionUtil.Framework45)
            {
                for (Control control = this; control != null; control = control.Parent)
                {
                    System.Web.UI.ValidateRequestMode validateRequestMode = control.ValidateRequestMode;
                    if (validateRequestMode != System.Web.UI.ValidateRequestMode.Inherit)
                    {
                        return (validateRequestMode == System.Web.UI.ValidateRequestMode.Enabled);
                    }
                }
            }
            return true;
        }

        protected void ClearCachedClientID()
        {
            this._cachedPredictableID = null;
            if (this.HasControls())
            {
                foreach (Control control in this.Controls)
                {
                    control.ClearCachedClientID();
                }
            }
        }

        private void ClearCachedUniqueIDRecursive()
        {
            this._cachedUniqueID = null;
            if (this._occasionalFields != null)
            {
                this._occasionalFields.UniqueIDPrefix = null;
            }
            if (this._controls != null)
            {
                int count = this._controls.Count;
                for (int i = 0; i < count; i++)
                {
                    this._controls[i].ClearCachedUniqueIDRecursive();
                }
            }
        }

        protected void ClearChildControlState()
        {
            if (this.ControlState >= System.Web.UI.ControlState.Initialized)
            {
                this.flags.Set(0x40000);
                if (this.Page != null)
                {
                    this.Page.RegisterRequiresClearChildControlState(this);
                }
            }
        }

        protected void ClearChildState()
        {
            this.ClearChildControlState();
            this.ClearChildViewState();
        }

        protected void ClearChildViewState()
        {
            if (this._occasionalFields != null)
            {
                this._occasionalFields.ControlsViewState = null;
            }
        }

        protected void ClearEffectiveClientIDMode()
        {
            this.EffectiveClientIDModeValue = System.Web.UI.ClientIDMode.Inherit;
            if (this.HasControls())
            {
                foreach (Control control in this.Controls)
                {
                    control.ClearEffectiveClientIDMode();
                }
            }
        }

        internal void ClearNamingContainer()
        {
            this.EnsureOccasionalFields();
            this._occasionalFields.NamedControlsID = 0;
            this.DirtyNameTable();
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected internal virtual void CreateChildControls()
        {
        }

        protected virtual ControlCollection CreateControlCollection()
        {
            return new ControlCollection(this);
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public virtual void DataBind()
        {
            this.DataBind(true);
        }

        protected virtual void DataBind(bool raiseOnDataBinding)
        {
            bool flag = false;
            if (this.IsBindingContainer)
            {
                bool flag2;
                object dataItem = DataBinder.GetDataItem(this, out flag2);
                if (flag2 && (this.Page != null))
                {
                    this.Page.PushDataBindingContext(dataItem);
                    flag = true;
                }
            }
            try
            {
                if (raiseOnDataBinding)
                {
                    this.OnDataBinding(EventArgs.Empty);
                }
                this.DataBindChildren();
            }
            finally
            {
                if (flag)
                {
                    this.Page.PopDataBindingContext();
                }
            }
        }

        protected virtual void DataBindChildren()
        {
            if (this.HasControls())
            {
                string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                try
                {
                    try
                    {
                        int count = this._controls.Count;
                        for (int i = 0; i < count; i++)
                        {
                            this._controls[i].DataBind();
                        }
                    }
                    finally
                    {
                        this._controls.SetCollectionReadOnly(errorMsg);
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        internal void DirtyNameTable()
        {
            if (this._occasionalFields != null)
            {
                this._occasionalFields.NamedControls = null;
            }
        }

        public virtual void Dispose()
        {
            IContainer service = null;
            if (this.Site != null)
            {
                service = (IContainer) this.Site.GetService(typeof(IContainer));
                if (service != null)
                {
                    service.Remove(this);
                    EventHandler handler = this.Events[EventDisposed] as EventHandler;
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                }
            }
            if (this._occasionalFields != null)
            {
                this._occasionalFields.Dispose();
            }
            if (this._events != null)
            {
                this._events.Dispose();
                this._events = null;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        protected void EndRenderTracing(TextWriter writer, object traceObject)
        {
            RenderTraceListener.CurrentListeners.EndRendering(writer, traceObject);
        }

        protected virtual void EnsureChildControls()
        {
            if (!this.ChildControlsCreated && !this.flags[0x100])
            {
                this.flags.Set(0x100);
                try
                {
                    this.ResolveAdapter();
                    if (this.AdapterInternal != null)
                    {
                        this.AdapterInternal.CreateChildControls();
                    }
                    else
                    {
                        this.CreateChildControls();
                    }
                    this.ChildControlsCreated = true;
                }
                finally
                {
                    this.flags.Clear(0x100);
                }
            }
        }

        protected void EnsureID()
        {
            if (this._namingContainer != null)
            {
                if (this._id == null)
                {
                    this.GenerateAutomaticID();
                }
                this.flags.Set(0x800);
            }
        }

        private void EnsureNamedControlsTable()
        {
            this._occasionalFields.NamedControls = new HybridDictionary(this._occasionalFields.NamedControlsID, true);
            this.FillNamedControlsTable(this, this._controls);
        }

        private void EnsureOccasionalFields()
        {
            if (this._occasionalFields == null)
            {
                this._occasionalFields = new OccasionalFields();
            }
        }

        internal int EstimateStateSize(object state)
        {
            if (state == null)
            {
                return 0;
            }
            return Util.SerializeWithAssert(new ObjectStateFormatter(), state).Length;
        }

        private void FillNamedControlsTable(Control namingContainer, ControlCollection controls)
        {
            int count = controls.Count;
            for (int i = 0; i < count; i++)
            {
                Control control = controls[i];
                if (control._id != null)
                {
                    try
                    {
                        namingContainer.EnsureOccasionalFields();
                        namingContainer._occasionalFields.NamedControls.Add(control._id, control);
                    }
                    catch
                    {
                        throw new HttpException(System.Web.SR.GetString("Duplicate_id_used", new object[] { control._id, "FindControl" }));
                    }
                }
                if (control.HasControls() && !control.flags[0x80])
                {
                    this.FillNamedControlsTable(namingContainer, control.Controls);
                }
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public virtual Control FindControl(string id)
        {
            return this.FindControl(id, 0);
        }

        protected virtual Control FindControl(string id, int pathOffset)
        {
            string str;
            RuntimeHelpers.EnsureSufficientExecutionStack();
            this.EnsureChildControls();
            if (!this.flags[0x80])
            {
                Control namingContainer = this.NamingContainer;
                if (namingContainer != null)
                {
                    return namingContainer.FindControl(id, pathOffset);
                }
                return null;
            }
            if (this.HasControls())
            {
                this.EnsureOccasionalFields();
                if (this._occasionalFields.NamedControls == null)
                {
                    this.EnsureNamedControlsTable();
                }
            }
            if ((this._occasionalFields == null) || (this._occasionalFields.NamedControls == null))
            {
                return null;
            }
            char[] anyOf = new char[] { '$', ':' };
            int num = id.IndexOfAny(anyOf, pathOffset);
            if (num == -1)
            {
                str = id.Substring(pathOffset);
                return (this._occasionalFields.NamedControls[str] as Control);
            }
            str = id.Substring(pathOffset, num - pathOffset);
            Control control2 = this._occasionalFields.NamedControls[str] as Control;
            if (control2 == null)
            {
                return null;
            }
            return control2.FindControl(id, num + 1);
        }

        internal Control FindControlFromPageIfNecessary(string id)
        {
            Control control = this.FindControl(id);
            if ((control == null) && (this.Page != null))
            {
                char[] anyOf = new char[] { '$', ':' };
                if (id.IndexOfAny(anyOf) != -1)
                {
                    control = this.Page.FindControl(id);
                }
            }
            return control;
        }

        public virtual void Focus()
        {
            this.Page.SetFocus(this);
        }

        private void GenerateAutomaticID()
        {
            this.flags.Set(0x200000);
            this._namingContainer.EnsureOccasionalFields();
            int index = this._namingContainer._occasionalFields.NamedControlsID++;
            if (this.EnableLegacyRendering)
            {
                this._id = "_ctl" + index.ToString(NumberFormatInfo.InvariantInfo);
            }
            else if (index < 0x80)
            {
                this._id = automaticIDs[index];
            }
            else
            {
                this._id = "ctl" + index.ToString(NumberFormatInfo.InvariantInfo);
            }
            this._namingContainer.DirtyNameTable();
        }

        private string GetClientID()
        {
            switch (this.EffectiveClientIDMode)
            {
                case System.Web.UI.ClientIDMode.Predictable:
                    return this.PredictableClientID;

                case System.Web.UI.ClientIDMode.Static:
                    return this.StaticClientID;
            }
            return this.UniqueClientID;
        }

        [SecurityPermission(SecurityAction.Demand, Unrestricted=true)]
        protected virtual IDictionary GetDesignModeState()
        {
            ControlRareFields rareFieldsEnsured = this.RareFieldsEnsured;
            if (rareFieldsEnsured.DesignModeState == null)
            {
                rareFieldsEnsured.DesignModeState = new HybridDictionary();
            }
            return rareFieldsEnsured.DesignModeState;
        }

        private string GetPredictableClientIDPrefix()
        {
            Control namingContainer = this.NamingContainer;
            if (namingContainer != null)
            {
                if (this._id == null)
                {
                    this.GenerateAutomaticID();
                }
                if ((namingContainer is System.Web.UI.Page) || (namingContainer is MasterPage))
                {
                    return this._id;
                }
                string clientID = namingContainer.GetClientID();
                if (string.IsNullOrEmpty(clientID))
                {
                    return this._id;
                }
                if (string.IsNullOrEmpty(this._id) || ((this is IDataItemContainer) && !(this is IDataBoundItemControl)))
                {
                    return clientID;
                }
                return (clientID + '_' + this._id);
            }
            return this._id;
        }

        private string GetPredictableClientIDSuffix()
        {
            string str = null;
            Control dataItemContainer = this.DataItemContainer;
            if (((dataItemContainer != null) && !(dataItemContainer is IDataBoundItemControl)) && (!(this is IDataItemContainer) || (this is IDataBoundItemControl)))
            {
                Control dataKeysContainer = dataItemContainer.DataKeysContainer;
                if (((dataKeysContainer != null) && (((IDataKeysControl) dataKeysContainer).ClientIDRowSuffix != null)) && (((IDataKeysControl) dataKeysContainer).ClientIDRowSuffix.Length > 0))
                {
                    str = string.Empty;
                    IOrderedDictionary values = ((IDataKeysControl) dataKeysContainer).ClientIDRowSuffixDataKeys[((IDataItemContainer) dataItemContainer).DisplayIndex].Values;
                    foreach (string str2 in ((IDataKeysControl) dataKeysContainer).ClientIDRowSuffix)
                    {
                        str = str + '_' + values[str2].ToString();
                    }
                    return str;
                }
                int displayIndex = ((IDataItemContainer) dataItemContainer).DisplayIndex;
                if (displayIndex >= 0)
                {
                    str = '_' + displayIndex.ToString(CultureInfo.InvariantCulture);
                }
            }
            return str;
        }

        public string GetRouteUrl(object routeParameters)
        {
            return this.GetRouteUrl(new RouteValueDictionary(routeParameters));
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public string GetRouteUrl(RouteValueDictionary routeParameters)
        {
            return this.GetRouteUrl(null, routeParameters);
        }

        public string GetRouteUrl(string routeName, object routeParameters)
        {
            return this.GetRouteUrl(routeName, new RouteValueDictionary(routeParameters));
        }

        public string GetRouteUrl(string routeName, RouteValueDictionary routeParameters)
        {
            VirtualPathData data = this.RouteCollection.GetVirtualPath(this.Context.Request.RequestContext, routeName, routeParameters);
            if (data != null)
            {
                return data.VirtualPath;
            }
            return null;
        }

        internal virtual System.Web.UI.TemplateControl GetTemplateControl()
        {
            if (((this._occasionalFields == null) || (this._occasionalFields.TemplateControl == null)) && (this.Parent != null))
            {
                System.Web.UI.TemplateControl templateControl = this.Parent.GetTemplateControl();
                if (templateControl != null)
                {
                    this.EnsureOccasionalFields();
                    this._occasionalFields.TemplateControl = templateControl;
                }
            }
            if (this._occasionalFields == null)
            {
                return null;
            }
            return this._occasionalFields.TemplateControl;
        }

        internal virtual string GetUniqueIDPrefix()
        {
            this.EnsureOccasionalFields();
            if (this._occasionalFields.UniqueIDPrefix == null)
            {
                string uniqueID = this.UniqueID;
                if (!string.IsNullOrEmpty(uniqueID))
                {
                    this._occasionalFields.UniqueIDPrefix = uniqueID + this.IdSeparator;
                }
                else
                {
                    this._occasionalFields.UniqueIDPrefix = string.Empty;
                }
            }
            return this._occasionalFields.UniqueIDPrefix;
        }

        public string GetUniqueIDRelativeTo(Control control)
        {
            if (control == null)
            {
                throw new ArgumentNullException("control");
            }
            if (!this.IsDescendentOf(control.NamingContainer))
            {
                throw new InvalidOperationException(System.Web.SR.GetString("Control_NotADescendentOfNamingContainer", new object[] { control.ID }));
            }
            if (control.NamingContainer == this.Page)
            {
                return this.UniqueID;
            }
            return this.UniqueID.Substring(control.NamingContainer.UniqueID.Length + 1);
        }

        internal XhtmlConformanceSection GetXhtmlConformanceSection()
        {
            HttpContext context = this.Context;
            if (context != null)
            {
                return System.Web.Configuration.RuntimeConfig.GetConfig(context).XhtmlConformance;
            }
            return System.Web.Configuration.RuntimeConfig.GetConfig().XhtmlConformance;
        }

        public virtual bool HasControls()
        {
            return ((this._controls != null) && (this._controls.Count > 0));
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected bool HasEvents()
        {
            return (this._events != null);
        }

        internal bool HasRenderDelegate()
        {
            return ((this.RareFields != null) && (this.RareFields.RenderMethod != null));
        }

        internal bool HasRenderingData()
        {
            if (!this.HasControls())
            {
                return this.HasRenderDelegate();
            }
            return true;
        }

        internal virtual void InitRecursive(Control namingContainer)
        {
            this.ResolveAdapter();
            if (this._controls != null)
            {
                if (this.flags[0x80])
                {
                    namingContainer = this;
                }
                string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this._controls.Count;
                for (int i = 0; i < count; i++)
                {
                    Control control = this._controls[i];
                    control.UpdateNamingContainer(namingContainer);
                    if (((control._id == null) && (namingContainer != null)) && !control.flags[0x40])
                    {
                        control.GenerateAutomaticID();
                    }
                    control._page = this.Page;
                    control.InitRecursive(namingContainer);
                }
                this._controls.SetCollectionReadOnly(errorMsg);
            }
            if (this._controlState < System.Web.UI.ControlState.Initialized)
            {
                this._controlState = System.Web.UI.ControlState.ChildrenInitialized;
                if (((this.Page != null) && !this.DesignMode) && (this.Page.ContainsTheme && this.EnableTheming))
                {
                    this.ApplySkin(this.Page);
                }
                if (this.AdapterInternal != null)
                {
                    this.AdapterInternal.OnInit(EventArgs.Empty);
                }
                else
                {
                    this.OnInit(EventArgs.Empty);
                }
                this._controlState = System.Web.UI.ControlState.Initialized;
            }
            this.TrackViewState();
        }

        internal async Task InitRecursiveAsync(Control namingContainer, System.Web.UI.Page page)
        {
            this.ResolveAdapter();
            if (this._controls != null)
            {
                if (this.flags[0x80])
                {
                    namingContainer = this;
                }
                string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this._controls.Count;
                for (int j = 0; j < count; j++)
                {
                    Control control = this._controls[j];
                    control.UpdateNamingContainer(namingContainer);
                    if (((control._id == null) && (namingContainer != null)) && !control.flags[0x40])
                    {
                        control.GenerateAutomaticID();
                    }
                    control._page = this.Page;
                    control.InitRecursive(namingContainer);
                }
                this._controls.SetCollectionReadOnly(errorMsg);
            }
            if (this._controlState < System.Web.UI.ControlState.Initialized)
            {
                this._controlState = System.Web.UI.ControlState.ChildrenInitialized;
                if (((this.Page != null) && !this.DesignMode) && (this.Page.ContainsTheme && this.EnableTheming))
                {
                    this.ApplySkin(this.Page);
                }
                using (page.Context.SyncContext.AllowVoidAsyncOperationsBlock())
                {
                    if (this.AdapterInternal != null)
                    {
                        this.AdapterInternal.OnInit(EventArgs.Empty);
                    }
                    else
                    {
                        this.OnInit(EventArgs.Empty);
                    }
                    await page.GetWaitForPreviousStepCompletionAwaitable();
                }
                this._controlState = System.Web.UI.ControlState.Initialized;
            }
            this.TrackViewState();
        }

        internal bool IsDescendentOf(Control ancestor)
        {
            Control parent = this;
            while ((parent != ancestor) && (parent.Parent != null))
            {
                parent = parent.Parent;
            }
            return (parent == ancestor);
        }

        protected bool IsLiteralContent()
        {
            return (((this._controls != null) && (this._controls.Count == 1)) && (this._controls[0] is LiteralControl));
        }

        internal void LoadChildViewStateByID(ArrayList childState)
        {
            int count = childState.Count;
            for (int i = 0; i < count; i += 2)
            {
                string id = (string) childState[i];
                object savedState = childState[i + 1];
                Control control = this.FindControl(id);
                if (control != null)
                {
                    control.LoadViewStateRecursive(savedState);
                }
                else
                {
                    this.EnsureOccasionalFields();
                    if (this._occasionalFields.ControlsViewState == null)
                    {
                        this._occasionalFields.ControlsViewState = new Hashtable();
                    }
                    this._occasionalFields.ControlsViewState[id] = savedState;
                }
            }
        }

        internal void LoadChildViewStateByIndex(ArrayList childState)
        {
            ControlCollection controls = this.Controls;
            int count = controls.Count;
            int num2 = childState.Count;
            for (int i = 0; i < num2; i += 2)
            {
                int num4 = (int) childState[i];
                object savedState = childState[i + 1];
                if (num4 < count)
                {
                    controls[num4].LoadViewStateRecursive(savedState);
                }
                else
                {
                    this.EnsureOccasionalFields();
                    if (this._occasionalFields.ControlsViewState == null)
                    {
                        this._occasionalFields.ControlsViewState = new Hashtable();
                    }
                    this._occasionalFields.ControlsViewState[num4] = savedState;
                }
            }
        }

        protected internal virtual void LoadControlState(object savedState)
        {
        }

        internal void LoadControlStateInternal(object savedStateObj)
        {
            if (!this.flags[0x100000])
            {
                this.flags.Set(0x100000);
                Pair pair = (Pair) savedStateObj;
                if (pair != null)
                {
                    System.Web.UI.Page page = this.Page;
                    if ((page == null) || page.ShouldLoadControlState(this))
                    {
                        if (pair.First != null)
                        {
                            this.LoadControlState(pair.First);
                        }
                        if ((this.AdapterInternal != null) && (pair.Second != null))
                        {
                            this.AdapterInternal.LoadAdapterControlState(pair.Second);
                        }
                    }
                }
            }
        }

        internal virtual void LoadRecursive()
        {
            if (this._controlState < System.Web.UI.ControlState.Loaded)
            {
                if (this.AdapterInternal != null)
                {
                    this.AdapterInternal.OnLoad(EventArgs.Empty);
                }
                else
                {
                    this.OnLoad(EventArgs.Empty);
                }
            }
            if (this._controls != null)
            {
                string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this._controls.Count;
                for (int i = 0; i < count; i++)
                {
                    this._controls[i].LoadRecursive();
                }
                this._controls.SetCollectionReadOnly(errorMsg);
            }
            if (this._controlState < System.Web.UI.ControlState.Loaded)
            {
                this._controlState = System.Web.UI.ControlState.Loaded;
            }
        }

        internal async Task LoadRecursiveAsync(System.Web.UI.Page page)
        {
            if (this._controlState < System.Web.UI.ControlState.Loaded)
            {
                using (page.Context.SyncContext.AllowVoidAsyncOperationsBlock())
                {
                    if (this.AdapterInternal != null)
                    {
                        this.AdapterInternal.OnLoad(EventArgs.Empty);
                    }
                    else
                    {
                        this.OnLoad(EventArgs.Empty);
                    }
                    await page.GetWaitForPreviousStepCompletionAwaitable();
                }
            }
            if (this._controls != null)
            {
                string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this._controls.Count;
                for (int j = 0; j < count; j++)
                {
                    this._controls[j].LoadRecursive();
                }
                this._controls.SetCollectionReadOnly(errorMsg);
            }
            if (this._controlState < System.Web.UI.ControlState.Loaded)
            {
                this._controlState = System.Web.UI.ControlState.Loaded;
            }
        }

        protected virtual void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                this.ViewState.LoadViewState(savedState);
                object obj2 = this.ViewState["Visible"];
                if (obj2 != null)
                {
                    if (!((bool) obj2))
                    {
                        this.flags.Set(0x10);
                    }
                    else
                    {
                        this.flags.Clear(0x10);
                    }
                    this.flags.Set(0x20);
                }
                object obj3 = this.ViewState["ValidateRequestMode"];
                if (obj3 != null)
                {
                    this.flags[0x60000000, 0x1d] = (int) obj3;
                    this.flags.Set(0x400000);
                }
            }
        }

        internal void LoadViewStateRecursive(object savedState)
        {
            if ((savedState != null) && !this.flags[4])
            {
                if ((this.Page != null) && this.Page.IsPostBack)
                {
                    object first = null;
                    object state = null;
                    ArrayList childState = null;
                    Pair pair = savedState as Pair;
                    if (pair != null)
                    {
                        first = pair.First;
                        childState = (ArrayList) pair.Second;
                    }
                    else
                    {
                        Triplet triplet = (Triplet) savedState;
                        first = triplet.First;
                        state = triplet.Second;
                        childState = (ArrayList) triplet.Third;
                    }
                    try
                    {
                        if ((state != null) && (this.AdapterInternal != null))
                        {
                            this.AdapterInternal.LoadAdapterViewState(state);
                        }
                        if (first != null)
                        {
                            this.LoadViewState(first);
                        }
                        if (childState != null)
                        {
                            if (this.LoadViewStateByID)
                            {
                                this.LoadChildViewStateByID(childState);
                            }
                            else
                            {
                                this.LoadChildViewStateByIndex(childState);
                            }
                        }
                    }
                    catch (InvalidCastException)
                    {
                        throw new HttpException(System.Web.SR.GetString("Controls_Cant_Change_Between_Posts"));
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new HttpException(System.Web.SR.GetString("Controls_Cant_Change_Between_Posts"));
                    }
                }
                this._controlState = System.Web.UI.ControlState.ViewStateLoaded;
            }
        }

        protected internal string MapPathSecure(string virtualPath)
        {
            string str;
            VirtualPath path;
            if (string.IsNullOrEmpty(virtualPath))
            {
                throw new ArgumentNullException("virtualPath", System.Web.SR.GetString("VirtualPath_Length_Zero"));
            }
            this.ResolvePhysicalOrVirtualPath(virtualPath, out path, out str);
            if (str == null)
            {
                str = path.MapPathInternal(this.TemplateControlVirtualDirectory, true);
            }
            HttpRuntime.CheckFilePermission(str);
            return str;
        }

        protected virtual bool OnBubbleEvent(object source, EventArgs args)
        {
            return false;
        }

        protected virtual void OnDataBinding(EventArgs e)
        {
            if (this.HasEvents())
            {
                EventHandler handler = this._events[EventDataBinding] as EventHandler;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal virtual void OnInit(EventArgs e)
        {
            if (this.HasEvents())
            {
                EventHandler handler = this._events[EventInit] as EventHandler;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal virtual void OnLoad(EventArgs e)
        {
            if (this.HasEvents())
            {
                EventHandler handler = this._events[EventLoad] as EventHandler;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal virtual void OnPreRender(EventArgs e)
        {
            if (this.HasEvents())
            {
                EventHandler handler = this._events[EventPreRender] as EventHandler;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal virtual void OnUnload(EventArgs e)
        {
            if (this.HasEvents())
            {
                EventHandler handler = this._events[EventUnload] as EventHandler;
                if (handler != null)
                {
                    handler(this, e);
                }
            }
        }

        protected internal Stream OpenFile(string path)
        {
            string physicalPath = null;
            VirtualFile file = null;
            path = path.Trim();
            if (UrlPath.IsAbsolutePhysicalPath(path))
            {
                physicalPath = path;
            }
            else
            {
                file = HostingEnvironment.VirtualPathProvider.GetFile(path);
                MapPathBasedVirtualFile file2 = file as MapPathBasedVirtualFile;
                if (file2 != null)
                {
                    physicalPath = file2.PhysicalPath;
                }
            }
            if (physicalPath != null)
            {
                HttpRuntime.CheckFilePermission(physicalPath);
            }
            if (file != null)
            {
                return file.Open();
            }
            return new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        internal Stream OpenFileAndGetDependency(VirtualPath virtualPath, string physicalPath, out CacheDependency dependency)
        {
            Stream stream;
            if ((physicalPath == null) && HostingEnvironment.UsingMapPathBasedVirtualPathProvider)
            {
                physicalPath = virtualPath.MapPathInternal(this.TemplateControlVirtualDirectory, true);
            }
            if (physicalPath != null)
            {
                HttpRuntime.CheckFilePermission(physicalPath);
                stream = new FileStream(physicalPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                dependency = new CacheDependency(0, physicalPath);
                return stream;
            }
            stream = virtualPath.OpenFile();
            dependency = VirtualPathProvider.GetCacheDependency(virtualPath);
            return stream;
        }

        internal virtual void PreRenderRecursiveInternal()
        {
            if (!this.Visible)
            {
                this.flags.Set(0x10);
            }
            else
            {
                this.flags.Clear(0x10);
                this.EnsureChildControls();
                if (this.AdapterInternal != null)
                {
                    this.AdapterInternal.OnPreRender(EventArgs.Empty);
                }
                else
                {
                    this.OnPreRender(EventArgs.Empty);
                }
                if (this._controls != null)
                {
                    string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                    int count = this._controls.Count;
                    for (int i = 0; i < count; i++)
                    {
                        this._controls[i].PreRenderRecursiveInternal();
                    }
                    this._controls.SetCollectionReadOnly(errorMsg);
                }
            }
            this._controlState = System.Web.UI.ControlState.PreRendered;
        }

        internal async Task PreRenderRecursiveInternalAsync(System.Web.UI.Page page)
        {
            if (!this.Visible)
            {
                this.flags.Set(0x10);
            }
            else
            {
                this.flags.Clear(0x10);
                this.EnsureChildControls();
                using (page.Context.SyncContext.AllowVoidAsyncOperationsBlock())
                {
                    if (this.AdapterInternal != null)
                    {
                        this.AdapterInternal.OnPreRender(EventArgs.Empty);
                    }
                    else
                    {
                        this.OnPreRender(EventArgs.Empty);
                    }
                    await page.GetWaitForPreviousStepCompletionAwaitable();
                }
                if (this._controls != null)
                {
                    string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                    int count = this._controls.Count;
                    for (int j = 0; j < count; j++)
                    {
                        this._controls[j].PreRenderRecursiveInternal();
                    }
                    this._controls.SetCollectionReadOnly(errorMsg);
                }
            }
            this._controlState = System.Web.UI.ControlState.PreRendered;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        internal void PreventAutoID()
        {
            if (!this.flags[0x80])
            {
                this.flags.Set(0x40);
            }
        }

        protected void RaiseBubbleEvent(object source, EventArgs args)
        {
            for (Control control = this.Parent; control != null; control = control.Parent)
            {
                if (control.OnBubbleEvent(source, args))
                {
                    return;
                }
            }
        }

        protected internal virtual void RemovedControl(Control control)
        {
            if (control.OwnerControl != null)
            {
                throw new InvalidOperationException(System.Web.SR.GetString("Substitution_NotAllowed"));
            }
            if ((this._namingContainer != null) && (control._id != null))
            {
                this._namingContainer.DirtyNameTable();
            }
            control.UnloadRecursive(false);
            control._parent = null;
            control._page = null;
            control._namingContainer = null;
            if (!(control is System.Web.UI.TemplateControl) && (control._occasionalFields != null))
            {
                control._occasionalFields.TemplateSourceVirtualDirectory = null;
            }
            if (control._occasionalFields != null)
            {
                control._occasionalFields.TemplateControl = null;
            }
            control.flags.Clear(0x800);
            control.ClearCachedUniqueIDRecursive();
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        protected internal virtual void Render(HtmlTextWriter writer)
        {
            this.RenderChildren(writer);
        }

        protected internal virtual void RenderChildren(HtmlTextWriter writer)
        {
            ICollection children = this._controls;
            this.RenderChildrenInternal(writer, children);
        }

        internal void RenderChildrenInternal(HtmlTextWriter writer, ICollection children)
        {
            if ((this.RareFields != null) && (this.RareFields.RenderMethod != null))
            {
                writer.BeginRender();
                this.RareFields.RenderMethod(writer, this);
                writer.EndRender();
            }
            else if (children != null)
            {
                foreach (Control control in children)
                {
                    control.RenderControl(writer);
                }
            }
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public virtual void RenderControl(HtmlTextWriter writer)
        {
            this.RenderControl(writer, this.Adapter);
        }

        protected void RenderControl(HtmlTextWriter writer, ControlAdapter adapter)
        {
            if (!this.flags[0x10] && !this.flags[0x200])
            {
                HttpContext context = (this.Page == null) ? null : this.Page._context;
                if ((context != null) && context.TraceIsEnabled)
                {
                    int bufferedLength = context.Response.GetBufferedLength();
                    this.RenderControlInternal(writer, adapter);
                    int num2 = context.Response.GetBufferedLength();
                    context.Trace.AddControlSize(this.UniqueID, num2 - bufferedLength);
                }
                else
                {
                    this.RenderControlInternal(writer, adapter);
                }
            }
            else
            {
                this.TraceNonRenderingControlInternal(writer);
            }
        }

        private void RenderControlInternal(HtmlTextWriter writer, ControlAdapter adapter)
        {
            try
            {
                this.BeginRenderTracing(writer, this);
                if (adapter != null)
                {
                    adapter.BeginRender(writer);
                    adapter.Render(writer);
                    adapter.EndRender(writer);
                }
                else
                {
                    this.Render(writer);
                }
            }
            finally
            {
                this.EndRenderTracing(writer, this);
            }
        }

        private void ResetVisible()
        {
            this.Visible = true;
        }

        protected virtual ControlAdapter ResolveAdapter()
        {
            if (!this.flags[0x8000])
            {
                if (this.DesignMode)
                {
                    this.flags.Set(0x8000);
                    return null;
                }
                HttpContext context = this.Context;
                if ((context != null) && (context.Request.Browser != null))
                {
                    this.AdapterInternal = context.Request.Browser.GetAdapter(this);
                }
                this.flags.Set(0x8000);
            }
            return this.AdapterInternal;
        }

        public string ResolveClientUrl(string relativeUrl)
        {
            if ((this.DesignMode && (this.Page != null)) && (this.Page.Site != null))
            {
                IUrlResolutionService service = (IUrlResolutionService) this.Page.Site.GetService(typeof(IUrlResolutionService));
                if (service != null)
                {
                    return service.ResolveClientUrl(relativeUrl);
                }
            }
            if (relativeUrl == null)
            {
                throw new ArgumentNullException("relativeUrl");
            }
            string virtualPathString = VirtualPath.GetVirtualPathString(this.TemplateControlVirtualDirectory);
            if (string.IsNullOrEmpty(virtualPathString))
            {
                return relativeUrl;
            }
            string str2 = this.Context.Request.ClientBaseDir.VirtualPathString;
            if (!UrlPath.IsAppRelativePath(relativeUrl))
            {
                if (StringUtil.EqualsIgnoreCase(str2, virtualPathString))
                {
                    return relativeUrl;
                }
                if ((relativeUrl.Length == 0) || !UrlPath.IsRelativeUrl(relativeUrl))
                {
                    return relativeUrl;
                }
            }
            string to = UrlPath.Combine(virtualPathString, relativeUrl);
            return HttpUtility.UrlPathEncode(UrlPath.MakeRelative(UrlPath.AppendSlashToPathIfNeeded(str2), to));
        }

        internal void ResolvePhysicalOrVirtualPath(string path, out VirtualPath virtualPath, out string physicalPath)
        {
            if (UrlPath.IsAbsolutePhysicalPath(path))
            {
                physicalPath = path;
                virtualPath = null;
            }
            else
            {
                physicalPath = null;
                virtualPath = this.TemplateControlVirtualDirectory.Combine(VirtualPath.Create(path));
            }
        }

        public string ResolveUrl(string relativeUrl)
        {
            if (relativeUrl == null)
            {
                throw new ArgumentNullException("relativeUrl");
            }
            if ((relativeUrl.Length == 0) || !UrlPath.IsRelativeUrl(relativeUrl))
            {
                return relativeUrl;
            }
            string appRelativeTemplateSourceDirectory = this.AppRelativeTemplateSourceDirectory;
            if (string.IsNullOrEmpty(appRelativeTemplateSourceDirectory))
            {
                return relativeUrl;
            }
            string virtualPath = UrlPath.Combine(appRelativeTemplateSourceDirectory, relativeUrl);
            return this.Context.Response.ApplyAppPathModifier(virtualPath);
        }

        protected internal virtual object SaveControlState()
        {
            return null;
        }

        internal object SaveControlStateInternal()
        {
            object x = this.SaveControlState();
            object y = null;
            if (this.AdapterInternal != null)
            {
                y = this.AdapterInternal.SaveAdapterControlState();
            }
            if ((x == null) && (y == null))
            {
                return null;
            }
            return new Pair(x, y);
        }

        protected virtual object SaveViewState()
        {
            if (this.flags[0x20])
            {
                this.ViewState["Visible"] = !this.flags[0x10];
            }
            if (this.flags[0x400000])
            {
                this.ViewState["ValidateRequestMode"] = (int) this.ValidateRequestMode;
            }
            if (this._viewState != null)
            {
                return this._viewState.SaveViewState();
            }
            return null;
        }

        internal object SaveViewStateRecursive(System.Web.UI.ViewStateMode inheritedMode)
        {
            if (!this.flags[4])
            {
                bool flag;
                if (this.flags[0x800000])
                {
                    if (this.flags[0x1000000])
                    {
                        flag = true;
                        inheritedMode = System.Web.UI.ViewStateMode.Enabled;
                    }
                    else
                    {
                        flag = false;
                        inheritedMode = System.Web.UI.ViewStateMode.Disabled;
                    }
                }
                else
                {
                    flag = inheritedMode == System.Web.UI.ViewStateMode.Enabled;
                }
                object y = null;
                object x = null;
                if (flag)
                {
                    if (this.AdapterInternal != null)
                    {
                        y = this.AdapterInternal.SaveAdapterViewState();
                    }
                    x = this.SaveViewState();
                }
                ArrayList z = null;
                if (this.HasControls())
                {
                    ControlCollection controls = this._controls;
                    int count = controls.Count;
                    bool loadViewStateByID = this.LoadViewStateByID;
                    for (int i = 0; i < count; i++)
                    {
                        Control control = controls[i];
                        object obj4 = control.SaveViewStateRecursive(inheritedMode);
                        if (obj4 != null)
                        {
                            if (z == null)
                            {
                                z = new ArrayList(count);
                            }
                            if (loadViewStateByID)
                            {
                                control.EnsureID();
                                z.Add(control.ID);
                            }
                            else
                            {
                                z.Add(i);
                            }
                            z.Add(obj4);
                        }
                    }
                }
                if (this.AdapterInternal != null)
                {
                    if (((x != null) || (y != null)) || (z != null))
                    {
                        return new Triplet(x, y, z);
                    }
                }
                else if ((x != null) || (z != null))
                {
                    return new Pair(x, z);
                }
            }
            return null;
        }

        internal void SetControlBuilder(ControlBuilder controlBuilder)
        {
            this.RareFieldsEnsured.ControlBuilder = controlBuilder;
        }

        internal void SetDesignMode()
        {
            this.flags.Set(0x10000);
            this.flags.Set(0x20000);
        }

        protected virtual void SetDesignModeState(IDictionary data)
        {
        }

        internal void SetEnableViewStateInternal(bool value)
        {
            if (!value)
            {
                this.flags.Set(4);
            }
            else
            {
                this.flags.Clear(4);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void SetRenderMethodDelegate(RenderMethod renderMethod)
        {
            this.RareFieldsEnsured.RenderMethod = renderMethod;
            this.Controls.SetCollectionReadOnly("Collection_readonly_Codeblocks");
        }

        [EditorBrowsable(EditorBrowsableState.Never), TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public void SetTraceData(object traceDataKey, object traceDataValue)
        {
            this.SetTraceData(this, traceDataKey, traceDataValue);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
        {
            RenderTraceListener.CurrentListeners.SetTraceData(tracedObject, traceDataKey, traceDataValue);
        }

        internal void SetValidateRequestModeInternal(System.Web.UI.ValidateRequestMode value, bool setDirty)
        {
            if ((value < System.Web.UI.ValidateRequestMode.Inherit) || (value > System.Web.UI.ValidateRequestMode.Enabled))
            {
                throw new ArgumentOutOfRangeException("value");
            }
            int num = this.flags[0x60000000, 0x1d];
            if (setDirty && (num != value))
            {
                this.flags.Set(0x400000);
            }
            this.flags[0x60000000, 0x1d] = (int) value;
        }

        internal bool ShouldSerializeEnableTheming()
        {
            return this.flags[0x2000];
        }

        private bool ShouldSerializeVisible()
        {
            return this.flags[0x10];
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        IDictionary IControlDesignerAccessor.GetDesignModeState()
        {
            return this.GetDesignModeState();
        }

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        void IControlDesignerAccessor.SetDesignModeState(IDictionary data)
        {
            this.SetDesignModeState(data);
        }

        void IControlDesignerAccessor.SetOwnerControl(Control owner)
        {
            if (owner == this)
            {
                throw new ArgumentException(System.Web.SR.GetString("Control_CannotOwnSelf"), "owner");
            }
            this.OwnerControl = owner;
            this._parent = owner.Parent;
            this._page = owner.Page;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        void IParserAccessor.AddParsedSubObject(object obj)
        {
            this.AddParsedSubObject(obj);
        }

        private void TraceNonRenderingControlInternal(TextWriter writer)
        {
            this.BeginRenderTracing(writer, this);
            this.EndRenderTracing(writer, this);
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        protected virtual void TrackViewState()
        {
            if (this._viewState != null)
            {
                this._viewState.TrackViewState();
            }
            this.flags.Set(2);
        }

        internal virtual void UnloadRecursive(bool dispose)
        {
            System.Web.UI.Page page = this.Page;
            if ((page != null) && page.RequiresControlState(this))
            {
                page.UnregisterRequiresControlState(this);
                this.RareFieldsEnsured.RequiredControlState = true;
            }
            if (this.flags[0x200000])
            {
                this._id = null;
                this.flags.Clear(0x200000);
            }
            if (this._controls != null)
            {
                string errorMsg = this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                int count = this._controls.Count;
                for (int i = 0; i < count; i++)
                {
                    this._controls[i].UnloadRecursive(dispose);
                }
                this._controls.SetCollectionReadOnly(errorMsg);
            }
            if (this.AdapterInternal != null)
            {
                this.AdapterInternal.OnUnload(EventArgs.Empty);
            }
            else
            {
                this.OnUnload(EventArgs.Empty);
            }
            if (dispose)
            {
                this.Dispose();
            }
            if (this.IsReloadable)
            {
                this._controlState = System.Web.UI.ControlState.Constructed;
            }
        }

        private void UpdateNamingContainer(Control namingContainer)
        {
            if ((this._namingContainer == null) || ((this._namingContainer != null) && (this._namingContainer != namingContainer)))
            {
                this.ClearCachedUniqueIDRecursive();
            }
            if (this.EffectiveClientIDModeValue != System.Web.UI.ClientIDMode.Inherit)
            {
                this.ClearCachedClientID();
                this.ClearEffectiveClientIDMode();
            }
            this._namingContainer = namingContainer;
        }

        internal void ValidateEvent(string uniqueID)
        {
            this.ValidateEvent(uniqueID, string.Empty);
        }

        internal void ValidateEvent(string uniqueID, string eventArgument)
        {
            if ((this.Page != null) && this.SupportsEventValidation)
            {
                this.Page.ClientScript.ValidateEvent(uniqueID, eventArgument);
            }
        }

        protected ControlAdapter Adapter
        {
            get
            {
                if (!this.flags[0x8000])
                {
                    this.AdapterInternal = this.ResolveAdapter();
                    this.flags.Set(0x8000);
                }
                return this.AdapterInternal;
            }
        }

        internal ControlAdapter AdapterInternal
        {
            get
            {
                if (((this._occasionalFields != null) && (this._occasionalFields.RareFields != null)) && (this._occasionalFields.RareFields.Adapter != null))
                {
                    return this._occasionalFields.RareFields.Adapter;
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.RareFieldsEnsured.Adapter = value;
                }
                else if (((this._occasionalFields != null) && (this._occasionalFields.RareFields != null)) && (this._occasionalFields.RareFields.Adapter != null))
                {
                    this._occasionalFields.RareFields.Adapter = null;
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), WebSysDescription("Control_TemplateSourceDirectory"), Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public string AppRelativeTemplateSourceDirectory
        {
            get
            {
                return VirtualPath.GetAppRelativeVirtualPathStringOrEmpty(this.TemplateControlVirtualDirectory);
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                this.TemplateControlVirtualDirectory = VirtualPath.CreateNonRelativeAllowNull(value);
            }
        }

        [Bindable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Control BindingContainer
        {
            get
            {
                Control namingContainer = this.NamingContainer;
                while (namingContainer is INonBindingContainer)
                {
                    namingContainer = namingContainer.BindingContainer;
                }
                return namingContainer;
            }
        }

        protected bool ChildControlsCreated
        {
            get
            {
                return this.flags[8];
            }
            set
            {
                if (!value && this.flags[8])
                {
                    this.Controls.Clear();
                }
                if (value)
                {
                    this.flags.Set(8);
                }
                else
                {
                    this.flags.Clear(8);
                }
            }
        }

        [WebSysDescription("Control_ClientID"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string ClientID
        {
            get
            {
                if (this.EffectiveClientIDMode != System.Web.UI.ClientIDMode.Static)
                {
                    this.EnsureID();
                }
                return this.GetClientID();
            }
        }

        [WebSysDescription("Control_ClientIDMode"), DefaultValue(0), Themeable(false), WebCategory("Behavior")]
        public virtual System.Web.UI.ClientIDMode ClientIDMode
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                return this.ClientIDModeValue;
            }
            set
            {
                if (this.ClientIDModeValue != value)
                {
                    if (value != this.EffectiveClientIDModeValue)
                    {
                        this.ClearEffectiveClientIDMode();
                        this.ClearCachedClientID();
                    }
                    this.ClientIDModeValue = value;
                }
            }
        }

        private System.Web.UI.ClientIDMode ClientIDModeValue
        {
            get
            {
                return (System.Web.UI.ClientIDMode) this.flags[0x6000000, 0x19];
            }
            set
            {
                this.flags[0x6000000, 0x19] = (int) value;
            }
        }

        protected char ClientIDSeparator
        {
            get
            {
                return '_';
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected internal virtual HttpContext Context
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                System.Web.UI.Page page = this.Page;
                if (page != null)
                {
                    return page.Context;
                }
                return HttpContext.Current;
            }
        }

        [WebSysDescription("Control_Controls"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ControlCollection Controls
        {
            get
            {
                if (this._controls == null)
                {
                    this._controls = this.CreateControlCollection();
                }
                return this._controls;
            }
        }

        internal System.Web.UI.ControlState ControlState
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._controlState;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this._controlState = value;
            }
        }

        [Bindable(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public Control DataItemContainer
        {
            get
            {
                Control namingContainer = this.NamingContainer;
                while ((namingContainer != null) && !(namingContainer is IDataItemContainer))
                {
                    namingContainer = namingContainer.DataItemContainer;
                }
                return namingContainer;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Bindable(false)]
        public Control DataKeysContainer
        {
            get
            {
                Control namingContainer = this.NamingContainer;
                while ((namingContainer != null) && !(namingContainer is IDataKeysControl))
                {
                    namingContainer = namingContainer.DataKeysContainer;
                }
                return namingContainer;
            }
        }

        protected internal bool DesignMode
        {
            get
            {
                if (!this.flags[0x20000])
                {
                    System.Web.UI.Page page = this.Page;
                    if (page != null)
                    {
                        if (page.GetDesignModeInternal())
                        {
                            this.flags.Set(0x10000);
                        }
                        else
                        {
                            this.flags.Clear(0x10000);
                        }
                    }
                    else if (this.Site != null)
                    {
                        if (this.Site.DesignMode)
                        {
                            this.flags.Set(0x10000);
                        }
                        else
                        {
                            this.flags.Clear(0x10000);
                        }
                    }
                    else if ((this.Parent != null) && this.Parent.DesignMode)
                    {
                        this.flags.Set(0x10000);
                    }
                    this.flags.Set(0x20000);
                }
                return this.flags[0x10000];
            }
        }

        internal virtual System.Web.UI.ClientIDMode EffectiveClientIDMode
        {
            get
            {
                if (this.EffectiveClientIDModeValue == System.Web.UI.ClientIDMode.Inherit)
                {
                    this.EffectiveClientIDModeValue = this.ClientIDMode;
                    if (this.EffectiveClientIDModeValue == System.Web.UI.ClientIDMode.Inherit)
                    {
                        if (this.NamingContainer != null)
                        {
                            this.EffectiveClientIDModeValue = this.NamingContainer.EffectiveClientIDMode;
                        }
                        else
                        {
                            HttpContext context = this.Context;
                            if (context != null)
                            {
                                this.EffectiveClientIDModeValue = System.Web.Configuration.RuntimeConfig.GetConfig(context).Pages.ClientIDMode;
                            }
                            else
                            {
                                this.EffectiveClientIDModeValue = System.Web.Configuration.RuntimeConfig.GetConfig().Pages.ClientIDMode;
                            }
                        }
                    }
                }
                return this.EffectiveClientIDModeValue;
            }
        }

        private System.Web.UI.ClientIDMode EffectiveClientIDModeValue
        {
            get
            {
                return (System.Web.UI.ClientIDMode) this.flags[0x18000000, 0x1b];
            }
            set
            {
                this.flags[0x18000000, 0x1b] = (int) value;
            }
        }

        internal bool EnableLegacyRendering
        {
            get
            {
                System.Web.UI.Page page = this.Page;
                if (page != null)
                {
                    return (page.XhtmlConformanceMode == XhtmlConformanceMode.Legacy);
                }
                return ((!this.DesignMode && (this.Adapter == null)) && (this.GetXhtmlConformanceSection().Mode == XhtmlConformanceMode.Legacy));
            }
        }

        [WebSysDescription("Control_EnableTheming"), Browsable(false), DefaultValue(true), Themeable(false), WebCategory("Behavior")]
        public virtual bool EnableTheming
        {
            get
            {
                if (!this.flags[0x2000] && (this.Parent != null))
                {
                    return this.Parent.EnableTheming;
                }
                return !this.flags[0x1000];
            }
            set
            {
                if ((this._controlState >= System.Web.UI.ControlState.FrameworkInitialized) && !this.DesignMode)
                {
                    throw new InvalidOperationException(System.Web.SR.GetString("PropertySetBeforePreInitOrAddToControls", new object[] { "EnableTheming" }));
                }
                if (!value)
                {
                    this.flags.Set(0x1000);
                }
                else
                {
                    this.flags.Clear(0x1000);
                }
                this.flags.Set(0x2000);
            }
        }

        [Themeable(false), WebSysDescription("Control_MaintainState"), WebCategory("Behavior"), DefaultValue(true)]
        public virtual bool EnableViewState
        {
            get
            {
                return !this.flags[4];
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.SetEnableViewStateInternal(value);
            }
        }

        protected EventHandlerList Events
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                if (this._events == null)
                {
                    this._events = new EventHandlerList();
                }
                return this._events;
            }
        }

        protected bool HasChildViewState
        {
            get
            {
                return (((this._occasionalFields != null) && (this._occasionalFields.ControlsViewState != null)) && (this._occasionalFields.ControlsViewState.Count > 0));
            }
        }

        [Themeable(false), WebSysDescription("Control_ID"), ParenthesizePropertyName(true), MergableProperty(false), Filterable(false)]
        public virtual string ID
        {
            get
            {
                if (!this.flags[1] && !this.flags[0x800])
                {
                    return null;
                }
                return this._id;
            }
            set
            {
                if ((value != null) && (value.Length == 0))
                {
                    value = null;
                }
                string str = this._id;
                this._id = value;
                this.ClearCachedUniqueIDRecursive();
                this.flags.Set(1);
                this.flags.Clear(0x200000);
                if ((this._namingContainer != null) && (str != null))
                {
                    this._namingContainer.DirtyNameTable();
                }
                if ((str != null) && (str != this._id))
                {
                    this.ClearCachedClientID();
                }
            }
        }

        protected char IdSeparator
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                if (this.Page != null)
                {
                    return this.Page.IdSeparator;
                }
                return this.IdSeparatorFromConfig;
            }
        }

        internal char IdSeparatorFromConfig
        {
            get
            {
                if (!this.EnableLegacyRendering)
                {
                    return '$';
                }
                return ':';
            }
        }

        internal bool IsBindingContainer
        {
            get
            {
                return ((this is INamingContainer) && !(this is INonBindingContainer));
            }
        }

        protected internal bool IsChildControlStateCleared
        {
            get
            {
                return this.flags[0x40000];
            }
        }

        internal bool IsParentedToUpdatePanel
        {
            get
            {
                for (Control control = this.Parent; control != null; control = control.Parent)
                {
                    if (control is IUpdatePanel)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        internal virtual bool IsReloadable
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                return false;
            }
        }

        protected bool IsTrackingViewState
        {
            get
            {
                return this.flags[2];
            }
        }

        protected internal bool IsViewStateEnabled
        {
            get
            {
                for (Control control = this; control != null; control = control.Parent)
                {
                    if (!control.EnableViewState)
                    {
                        return false;
                    }
                    System.Web.UI.ViewStateMode viewStateMode = control.ViewStateMode;
                    if (viewStateMode != System.Web.UI.ViewStateMode.Inherit)
                    {
                        return (viewStateMode == System.Web.UI.ViewStateMode.Enabled);
                    }
                }
                return true;
            }
        }

        protected bool LoadViewStateByID
        {
            get
            {
                return ViewStateModeByIdAttribute.IsEnabled(base.GetType());
            }
        }

        [WebSysDescription("Control_NamingContainer"), Bindable(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Control NamingContainer
        {
            get
            {
                if ((this._namingContainer == null) && (this.Parent != null))
                {
                    if (this.Parent.flags[0x80])
                    {
                        this._namingContainer = this.Parent;
                    }
                    else
                    {
                        this._namingContainer = this.Parent.NamingContainer;
                    }
                }
                return this._namingContainer;
            }
        }

        private Control OwnerControl
        {
            get
            {
                if (this.RareFields == null)
                {
                    return null;
                }
                return this.RareFields.OwnerControl;
            }
            set
            {
                this.RareFieldsEnsured.OwnerControl = value;
            }
        }

        [Browsable(false), Bindable(false), WebSysDescription("Control_Page"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual System.Web.UI.Page Page
        {
            get
            {
                if ((this._page == null) && (this.Parent != null))
                {
                    this._page = this.Parent.Page;
                }
                return this._page;
            }
            set
            {
                if (this.OwnerControl != null)
                {
                    throw new InvalidOperationException();
                }
                this._page = value;
            }
        }

        [WebSysDescription("Control_Parent"), Bindable(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Control Parent
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                return this._parent;
            }
        }

        internal IPostBackDataHandler PostBackDataHandler
        {
            get
            {
                IPostBackDataHandler adapterInternal = this.AdapterInternal as IPostBackDataHandler;
                if (adapterInternal != null)
                {
                    return adapterInternal;
                }
                return (this as IPostBackDataHandler);
            }
        }

        internal IPostBackEventHandler PostBackEventHandler
        {
            get
            {
                IPostBackEventHandler adapterInternal = this.AdapterInternal as IPostBackEventHandler;
                if (adapterInternal != null)
                {
                    return adapterInternal;
                }
                return (this as IPostBackEventHandler);
            }
        }

        internal string PredictableClientID
        {
            get
            {
                if (this._cachedPredictableID != null)
                {
                    return this._cachedPredictableID;
                }
                this._cachedPredictableID = this.GetPredictableClientIDPrefix();
                string predictableClientIDSuffix = this.GetPredictableClientIDSuffix();
                if (!string.IsNullOrEmpty(predictableClientIDSuffix))
                {
                    if (!string.IsNullOrEmpty(this._cachedPredictableID))
                    {
                        this._cachedPredictableID = this._cachedPredictableID + predictableClientIDSuffix;
                    }
                    else
                    {
                        this._cachedPredictableID = predictableClientIDSuffix.Substring(1);
                    }
                }
                if (!string.IsNullOrEmpty(this._cachedPredictableID))
                {
                    return this._cachedPredictableID;
                }
                return string.Empty;
            }
        }

        private ControlRareFields RareFields
        {
            get
            {
                if (this._occasionalFields != null)
                {
                    return this._occasionalFields.RareFields;
                }
                return null;
            }
        }

        private ControlRareFields RareFieldsEnsured
        {
            get
            {
                this.EnsureOccasionalFields();
                ControlRareFields rareFields = this._occasionalFields.RareFields;
                if (rareFields == null)
                {
                    rareFields = new ControlRareFields();
                    this._occasionalFields.RareFields = rareFields;
                }
                return rareFields;
            }
        }

        [Browsable(false), Bindable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Version RenderingCompatibility
        {
            get
            {
                if (((this._occasionalFields != null) && (this._occasionalFields.RareFields != null)) && (this._occasionalFields.RareFields.RenderingCompatibility != null))
                {
                    return this._occasionalFields.RareFields.RenderingCompatibility;
                }
                return this.RuntimeConfig.Pages.ControlRenderingCompatibilityVersion;
            }
            set
            {
                if (value != null)
                {
                    this.RareFieldsEnsured.RenderingCompatibility = value;
                }
                else if (((this._occasionalFields != null) && (this._occasionalFields.RareFields != null)) && (this._occasionalFields.RareFields.RenderingCompatibility != null))
                {
                    this._occasionalFields.RareFields.RenderingCompatibility = null;
                }
            }
        }

        internal System.Web.Routing.RouteCollection RouteCollection
        {
            get
            {
                if (((this._occasionalFields != null) && (this._occasionalFields.RareFields != null)) && (this._occasionalFields.RareFields.RouteCollection != null))
                {
                    return this._occasionalFields.RareFields.RouteCollection;
                }
                return RouteTable.Routes;
            }
            set
            {
                if (value != null)
                {
                    this.RareFieldsEnsured.RouteCollection = value;
                }
                else if (((this._occasionalFields != null) && (this._occasionalFields.RareFields != null)) && (this._occasionalFields.RareFields.RouteCollection != null))
                {
                    this._occasionalFields.RareFields.RouteCollection = null;
                }
            }
        }

        private System.Web.Configuration.RuntimeConfig RuntimeConfig
        {
            get
            {
                HttpContext context = this.Context;
                if (context != null)
                {
                    return System.Web.Configuration.RuntimeConfig.GetConfig(context);
                }
                return System.Web.Configuration.RuntimeConfig.GetConfig();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced), WebSysDescription("Control_Site"), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ISite Site
        {
            get
            {
                if (this.OwnerControl != null)
                {
                    return this.OwnerControl.Site;
                }
                if (this.RareFields != null)
                {
                    return this.RareFields.Site;
                }
                return null;
            }
            set
            {
                if (this.OwnerControl != null)
                {
                    throw new InvalidOperationException(System.Web.SR.GetString("Substitution_SiteNotAllowed"));
                }
                this.RareFieldsEnsured.Site = value;
                this.flags.Clear(0x20000);
            }
        }

        [DefaultValue(""), WebSysDescription("Control_SkinId"), Browsable(false), Filterable(false), WebCategory("Behavior")]
        public virtual string SkinID
        {
            get
            {
                if ((this._occasionalFields != null) && (this._occasionalFields.SkinId != null))
                {
                    return this._occasionalFields.SkinId;
                }
                return string.Empty;
            }
            set
            {
                if (!this.DesignMode)
                {
                    if (this.flags[0x4000])
                    {
                        throw new InvalidOperationException(System.Web.SR.GetString("PropertySetBeforeStyleSheetApplied", new object[] { "SkinId" }));
                    }
                    if (this._controlState >= System.Web.UI.ControlState.FrameworkInitialized)
                    {
                        throw new InvalidOperationException(System.Web.SR.GetString("PropertySetBeforePreInitOrAddToControls", new object[] { "SkinId" }));
                    }
                }
                this.EnsureOccasionalFields();
                this._occasionalFields.SkinId = value;
            }
        }

        internal string SpacerImageUrl
        {
            get
            {
                this.EnsureOccasionalFields();
                if (this._occasionalFields.SpacerImageUrl == null)
                {
                    this._occasionalFields.SpacerImageUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(WebControl), "Spacer.gif");
                }
                return this._occasionalFields.SpacerImageUrl;
            }
        }

        internal string StaticClientID
        {
            get
            {
                if (!this.flags[0x200000])
                {
                    string iD = this.ID;
                    if (iD != null)
                    {
                        return iD;
                    }
                    return string.Empty;
                }
                return string.Empty;
            }
        }

        private bool SupportsEventValidation
        {
            get
            {
                return SupportsEventValidationAttribute.SupportsEventValidation(base.GetType());
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        ControlBuilder IControlBuilderAccessor.ControlBuilder
        {
            get
            {
                if (this.RareFields == null)
                {
                    return null;
                }
                return this.RareFields.ControlBuilder;
            }
        }

        IDictionary IControlDesignerAccessor.UserData
        {
            get
            {
                ControlRareFields rareFieldsEnsured = this.RareFieldsEnsured;
                if (rareFieldsEnsured.ControlDesignerAccessorUserData == null)
                {
                    rareFieldsEnsured.ControlDesignerAccessorUserData = new HybridDictionary();
                }
                return rareFieldsEnsured.ControlDesignerAccessorUserData;
            }
        }

        DataBindingCollection IDataBindingsAccessor.DataBindings
        {
            get
            {
                ControlRareFields rareFieldsEnsured = this.RareFieldsEnsured;
                if (rareFieldsEnsured.DataBindings == null)
                {
                    rareFieldsEnsured.DataBindings = new DataBindingCollection();
                }
                return rareFieldsEnsured.DataBindings;
            }
        }

        bool IDataBindingsAccessor.HasDataBindings
        {
            get
            {
                return (((this.RareFields != null) && (this.RareFields.DataBindings != null)) && (this.RareFields.DataBindings.Count != 0));
            }
        }

        ExpressionBindingCollection IExpressionsAccessor.Expressions
        {
            get
            {
                ExpressionBindingCollection expressionBindings = this.RareFieldsEnsured.ExpressionBindings;
                if (expressionBindings == null)
                {
                    expressionBindings = new ExpressionBindingCollection();
                    this.RareFields.ExpressionBindings = expressionBindings;
                }
                return expressionBindings;
            }
        }

        bool IExpressionsAccessor.HasExpressions
        {
            get
            {
                if (this.RareFields == null)
                {
                    return false;
                }
                ExpressionBindingCollection expressionBindings = this.RareFields.ExpressionBindings;
                return ((expressionBindings != null) && (expressionBindings.Count > 0));
            }
        }

        [WebSysDescription("Control_TemplateControl"), Bindable(false), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Web.UI.TemplateControl TemplateControl
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.GetTemplateControl();
            }
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                if (value != null)
                {
                    this.EnsureOccasionalFields();
                    this._occasionalFields.TemplateControl = value;
                }
                else if ((this._occasionalFields != null) && (this._occasionalFields.TemplateControl != null))
                {
                    this._occasionalFields.TemplateControl = null;
                }
            }
        }

        internal VirtualPath TemplateControlVirtualDirectory
        {
            get
            {
                if ((this._occasionalFields == null) || (this._occasionalFields.TemplateSourceVirtualDirectory == null))
                {
                    System.Web.UI.TemplateControl templateControl = this.TemplateControl;
                    if (templateControl == null)
                    {
                        HttpContext context = this.Context;
                        if (context != null)
                        {
                            VirtualPath parent = context.Request.CurrentExecutionFilePathObject.Parent;
                            if (parent != null)
                            {
                                this.EnsureOccasionalFields();
                                this._occasionalFields.TemplateSourceVirtualDirectory = parent;
                            }
                        }
                        if (this._occasionalFields == null)
                        {
                            return null;
                        }
                        return this._occasionalFields.TemplateSourceVirtualDirectory;
                    }
                    if (templateControl != this)
                    {
                        VirtualPath templateControlVirtualDirectory = templateControl.TemplateControlVirtualDirectory;
                        if (templateControlVirtualDirectory != null)
                        {
                            this.EnsureOccasionalFields();
                            this._occasionalFields.TemplateSourceVirtualDirectory = templateControlVirtualDirectory;
                        }
                    }
                    if (this._occasionalFields == null)
                    {
                        return null;
                    }
                }
                return this._occasionalFields.TemplateSourceVirtualDirectory;
            }
            set
            {
                if (value != null)
                {
                    this.EnsureOccasionalFields();
                    this._occasionalFields.TemplateSourceVirtualDirectory = value;
                }
                else if ((this._occasionalFields != null) && (this._occasionalFields.TemplateSourceVirtualDirectory != null))
                {
                    this._occasionalFields.TemplateSourceVirtualDirectory = null;
                }
            }
        }

        [WebSysDescription("Control_TemplateSourceDirectory"), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual string TemplateSourceDirectory
        {
            get
            {
                if (this.TemplateControlVirtualDirectory == null)
                {
                    return string.Empty;
                }
                return this.TemplateControlVirtualDirectory.VirtualPathStringNoTrailingSlash;
            }
        }

        internal string UniqueClientID
        {
            get
            {
                string uniqueID = this.UniqueID;
                if ((uniqueID != null) && (uniqueID.IndexOf(this.IdSeparator) >= 0))
                {
                    return uniqueID.Replace(this.IdSeparator, '_');
                }
                return uniqueID;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), WebSysDescription("Control_UniqueID")]
        public virtual string UniqueID
        {
            get
            {
                if (this._cachedUniqueID == null)
                {
                    Control namingContainer = this.NamingContainer;
                    if (namingContainer == null)
                    {
                        return this._id;
                    }
                    if (this._id == null)
                    {
                        this.GenerateAutomaticID();
                    }
                    if (this.Page == namingContainer)
                    {
                        this._cachedUniqueID = this._id;
                    }
                    else
                    {
                        string uniqueIDPrefix = namingContainer.GetUniqueIDPrefix();
                        if (uniqueIDPrefix.Length == 0)
                        {
                            return this._id;
                        }
                        this._cachedUniqueID = uniqueIDPrefix + this._id;
                    }
                }
                return this._cachedUniqueID;
            }
        }

        [WebCategory("Behavior"), DefaultValue(0), WebSysDescription("Control_ValidateRequestMode")]
        public virtual System.Web.UI.ValidateRequestMode ValidateRequestMode
        {
            get
            {
                return (System.Web.UI.ValidateRequestMode) this.flags[0x60000000, 0x1d];
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this.SetValidateRequestModeInternal(value, true);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), WebSysDescription("Control_State")]
        protected virtual StateBag ViewState
        {
            get
            {
                if (this._viewState == null)
                {
                    this._viewState = new StateBag(this.ViewStateIgnoresCase);
                    if (this.IsTrackingViewState)
                    {
                        this._viewState.TrackViewState();
                    }
                }
                return this._viewState;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        protected virtual bool ViewStateIgnoresCase
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get
            {
                return false;
            }
        }

        [DefaultValue(0), Themeable(false), WebCategory("Behavior"), WebSysDescription("Control_ViewStateMode")]
        public virtual System.Web.UI.ViewStateMode ViewStateMode
        {
            get
            {
                if (!this.flags[0x800000])
                {
                    return System.Web.UI.ViewStateMode.Inherit;
                }
                if (!this.flags[0x1000000])
                {
                    return System.Web.UI.ViewStateMode.Disabled;
                }
                return System.Web.UI.ViewStateMode.Enabled;
            }
            set
            {
                if ((value < System.Web.UI.ViewStateMode.Inherit) || (value > System.Web.UI.ViewStateMode.Disabled))
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                if (value == System.Web.UI.ViewStateMode.Inherit)
                {
                    this.flags.Clear(0x800000);
                }
                else
                {
                    this.flags.Set(0x800000);
                    if (value == System.Web.UI.ViewStateMode.Enabled)
                    {
                        this.flags.Set(0x1000000);
                    }
                    else
                    {
                        this.flags.Clear(0x1000000);
                    }
                }
            }
        }

        [DefaultValue(true), Bindable(true), WebCategory("Behavior"), WebSysDescription("Control_Visible")]
        public virtual bool Visible
        {
            get
            {
                if (this.flags[0x10])
                {
                    return false;
                }
                if ((this._parent != null) && !this.DesignMode)
                {
                    return this._parent.Visible;
                }
                return true;
            }
            set
            {
                if (this.flags[2])
                {
                    bool flag = !this.flags[0x10];
                    if (flag != value)
                    {
                        this.flags.Set(0x20);
                    }
                }
                if (!value)
                {
                    this.flags.Set(0x10);
                }
                else
                {
                    this.flags.Clear(0x10);
                }
            }
        }

        [CompilerGenerated]
        private struct <InitRecursiveAsync>d__0 : IAsyncStateMachine
        {
            public int <>1__state;
            public Control <>4__this;
            public IDisposable <>7__wrap1;
            public AsyncTaskMethodBuilder <>t__builder;
            private object <>t__stack;
            private WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter <>u__$awaiter2;
            public Control namingContainer;
            public Page page;

            private void MoveNext()
            {
                try
                {
                    bool flag = true;
                    if (this.<>1__state != 0)
                    {
                        this.<>4__this.ResolveAdapter();
                        if (this.<>4__this._controls != null)
                        {
                            if (this.<>4__this.flags[0x80])
                            {
                                this.namingContainer = this.<>4__this;
                            }
                            string errorMsg = this.<>4__this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                            int count = this.<>4__this._controls.Count;
                            for (int i = 0; i < count; i++)
                            {
                                Control control = this.<>4__this._controls[i];
                                control.UpdateNamingContainer(this.namingContainer);
                                if (((control._id == null) && (this.namingContainer != null)) && !control.flags[0x40])
                                {
                                    control.GenerateAutomaticID();
                                }
                                control._page = this.<>4__this.Page;
                                control.InitRecursive(this.namingContainer);
                            }
                            this.<>4__this._controls.SetCollectionReadOnly(errorMsg);
                        }
                        if (this.<>4__this._controlState >= ControlState.Initialized)
                        {
                            goto Label_0257;
                        }
                        this.<>4__this._controlState = ControlState.ChildrenInitialized;
                        if (((this.<>4__this.Page != null) && !this.<>4__this.DesignMode) && (this.<>4__this.Page.ContainsTheme && this.<>4__this.EnableTheming))
                        {
                            this.<>4__this.ApplySkin(this.<>4__this.Page);
                        }
                        this.<>7__wrap1 = this.page.Context.SyncContext.AllowVoidAsyncOperationsBlock();
                    }
                    try
                    {
                        WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter awaiter;
                        if (this.<>1__state != 0)
                        {
                            if (this.<>4__this.AdapterInternal != null)
                            {
                                this.<>4__this.AdapterInternal.OnInit(EventArgs.Empty);
                            }
                            else
                            {
                                this.<>4__this.OnInit(EventArgs.Empty);
                            }
                            awaiter = this.page.GetWaitForPreviousStepCompletionAwaitable().GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = 0;
                                this.<>u__$awaiter2 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter, Control.<InitRecursiveAsync>d__0>(ref awaiter, ref this);
                                flag = false;
                                return;
                            }
                        }
                        else
                        {
                            awaiter = this.<>u__$awaiter2;
                            this.<>u__$awaiter2 = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter();
                            this.<>1__state = -1;
                        }
                        awaiter.GetResult();
                        awaiter = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter();
                    }
                    finally
                    {
                        if (flag && (this.<>7__wrap1 != null))
                        {
                            this.<>7__wrap1.Dispose();
                        }
                    }
                    this.<>4__this._controlState = ControlState.Initialized;
                Label_0257:
                    this.<>4__this.TrackViewState();
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine param0)
            {
                this.<>t__builder.SetStateMachine(param0);
            }
        }

        [CompilerGenerated]
        private struct <LoadRecursiveAsync>d__4 : IAsyncStateMachine
        {
            public int <>1__state;
            public Control <>4__this;
            public IDisposable <>7__wrap5;
            public AsyncTaskMethodBuilder <>t__builder;
            private object <>t__stack;
            private WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter <>u__$awaiter6;
            public Page page;

            private void MoveNext()
            {
                try
                {
                    bool flag = true;
                    if (this.<>1__state != 0)
                    {
                        if (this.<>4__this._controlState >= ControlState.Loaded)
                        {
                            goto Label_0106;
                        }
                        this.<>7__wrap5 = this.page.Context.SyncContext.AllowVoidAsyncOperationsBlock();
                    }
                    try
                    {
                        WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter awaiter;
                        if (this.<>1__state != 0)
                        {
                            if (this.<>4__this.AdapterInternal != null)
                            {
                                this.<>4__this.AdapterInternal.OnLoad(EventArgs.Empty);
                            }
                            else
                            {
                                this.<>4__this.OnLoad(EventArgs.Empty);
                            }
                            awaiter = this.page.GetWaitForPreviousStepCompletionAwaitable().GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = 0;
                                this.<>u__$awaiter6 = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter, Control.<LoadRecursiveAsync>d__4>(ref awaiter, ref this);
                                flag = false;
                                return;
                            }
                        }
                        else
                        {
                            awaiter = this.<>u__$awaiter6;
                            this.<>u__$awaiter6 = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter();
                            this.<>1__state = -1;
                        }
                        awaiter.GetResult();
                        awaiter = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter();
                    }
                    finally
                    {
                        if (flag && (this.<>7__wrap5 != null))
                        {
                            this.<>7__wrap5.Dispose();
                        }
                    }
                Label_0106:
                    if (this.<>4__this._controls != null)
                    {
                        string errorMsg = this.<>4__this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                        int count = this.<>4__this._controls.Count;
                        for (int i = 0; i < count; i++)
                        {
                            this.<>4__this._controls[i].LoadRecursive();
                        }
                        this.<>4__this._controls.SetCollectionReadOnly(errorMsg);
                    }
                    if (this.<>4__this._controlState < ControlState.Loaded)
                    {
                        this.<>4__this._controlState = ControlState.Loaded;
                    }
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine param0)
            {
                this.<>t__builder.SetStateMachine(param0);
            }
        }

        [CompilerGenerated]
        private struct <PreRenderRecursiveInternalAsync>d__8 : IAsyncStateMachine
        {
            public int <>1__state;
            public Control <>4__this;
            public IDisposable <>7__wrapa;
            public AsyncTaskMethodBuilder <>t__builder;
            private object <>t__stack;
            private WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter <>u__$awaiterb;
            public bool <visible>5__9;
            public Page page;

            private void MoveNext()
            {
                try
                {
                    bool flag = true;
                    if (this.<>1__state != 0)
                    {
                        this.<visible>5__9 = this.<>4__this.Visible;
                        if (!this.<visible>5__9)
                        {
                            this.<>4__this.flags.Set(0x10);
                            goto Label_01AA;
                        }
                        this.<>4__this.flags.Clear(0x10);
                        this.<>4__this.EnsureChildControls();
                        this.<>7__wrapa = this.page.Context.SyncContext.AllowVoidAsyncOperationsBlock();
                    }
                    try
                    {
                        WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter awaiter;
                        if (this.<>1__state != 0)
                        {
                            if (this.<>4__this.AdapterInternal != null)
                            {
                                this.<>4__this.AdapterInternal.OnPreRender(EventArgs.Empty);
                            }
                            else
                            {
                                this.<>4__this.OnPreRender(EventArgs.Empty);
                            }
                            awaiter = this.page.GetWaitForPreviousStepCompletionAwaitable().GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                this.<>1__state = 0;
                                this.<>u__$awaiterb = awaiter;
                                this.<>t__builder.AwaitUnsafeOnCompleted<WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter, Control.<PreRenderRecursiveInternalAsync>d__8>(ref awaiter, ref this);
                                flag = false;
                                return;
                            }
                        }
                        else
                        {
                            awaiter = this.<>u__$awaiterb;
                            this.<>u__$awaiterb = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter();
                            this.<>1__state = -1;
                        }
                        awaiter.GetResult();
                        awaiter = new WithinCancellableCallbackTaskAwaitable.WithinCancellableCallbackTaskAwaiter();
                    }
                    finally
                    {
                        if (flag && (this.<>7__wrapa != null))
                        {
                            this.<>7__wrapa.Dispose();
                        }
                    }
                    if (this.<>4__this._controls != null)
                    {
                        string errorMsg = this.<>4__this._controls.SetCollectionReadOnly("Parent_collections_readonly");
                        int count = this.<>4__this._controls.Count;
                        for (int i = 0; i < count; i++)
                        {
                            this.<>4__this._controls[i].PreRenderRecursiveInternal();
                        }
                        this.<>4__this._controls.SetCollectionReadOnly(errorMsg);
                    }
                Label_01AA:
                    this.<>4__this._controlState = ControlState.PreRendered;
                }
                catch (Exception exception)
                {
                    this.<>1__state = -2;
                    this.<>t__builder.SetException(exception);
                    return;
                }
                this.<>1__state = -2;
                this.<>t__builder.SetResult();
            }

            [DebuggerHidden]
            private void SetStateMachine(IAsyncStateMachine param0)
            {
                this.<>t__builder.SetStateMachine(param0);
            }
        }

        private sealed class ControlRareFields : IDisposable
        {
            public ControlAdapter Adapter;
            public System.Web.UI.ControlBuilder ControlBuilder;
            public IDictionary ControlDesignerAccessorUserData;
            public DataBindingCollection DataBindings;
            public IDictionary DesignModeState;
            public ExpressionBindingCollection ExpressionBindings;
            public Control OwnerControl;
            public Version RenderingCompatibility;
            public System.Web.UI.RenderMethod RenderMethod;
            public bool RequiredControlState;
            public System.Web.Routing.RouteCollection RouteCollection;
            public ISite Site;

            internal ControlRareFields()
            {
            }

            public void Dispose()
            {
                this.ControlBuilder = null;
                if (this.OwnerControl != null)
                {
                    this.OwnerControl.Dispose();
                }
                this.ControlDesignerAccessorUserData = null;
                this.DesignModeState = null;
                this.RenderingCompatibility = null;
                this.RouteCollection = null;
            }
        }

        private sealed class OccasionalFields : IDisposable
        {
            public IDictionary ControlsViewState;
            public IDictionary NamedControls;
            public int NamedControlsID;
            public Control.ControlRareFields RareFields;
            public string SkinId;
            public string SpacerImageUrl;
            public System.Web.UI.TemplateControl TemplateControl;
            public VirtualPath TemplateSourceVirtualDirectory;
            public string UniqueIDPrefix;

            internal OccasionalFields()
            {
            }

            public void Dispose()
            {
                if (this.RareFields != null)
                {
                    this.RareFields.Dispose();
                }
                this.ControlsViewState = null;
            }
        }
    }
}
