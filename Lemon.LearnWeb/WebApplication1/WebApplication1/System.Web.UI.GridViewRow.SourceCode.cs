namespace System.Web.UI.WebControls
{
    using System;
    using System.Runtime;
    using System.Web.UI;

    public class GridViewRow : TableRow, IDataItemContainer, INamingContainer
    {
        private object _dataItem;
        private int _dataItemIndex;
        private int _rowIndex;
        private DataControlRowState _rowState;
        private DataControlRowType _rowType;

        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public GridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
        {
            this._rowIndex = rowIndex;
            this._dataItemIndex = dataItemIndex;
            this._rowType = rowType;
            this._rowState = rowState;
        }

        protected override bool OnBubbleEvent(object source, EventArgs e)
        {
            if (e is CommandEventArgs)
            {
                GridViewCommandEventArgs args = new GridViewCommandEventArgs(this, source, (CommandEventArgs) e);
                base.RaiseBubbleEvent(this, args);
                return true;
            }
            return false;
        }

        public virtual object DataItem
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._dataItem;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this._dataItem = value;
            }
        }

        public virtual int DataItemIndex
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._dataItemIndex;
            }
        }

        public virtual int RowIndex
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._rowIndex;
            }
        }

        public virtual DataControlRowState RowState
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._rowState;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this._rowState = value;
            }
        }

        public virtual DataControlRowType RowType
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this._rowType;
            }
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            set
            {
                this._rowType = value;
            }
        }

        object IDataItemContainer.DataItem
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.DataItem;
            }
        }

        int IDataItemContainer.DataItemIndex
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.DataItemIndex;
            }
        }

        int IDataItemContainer.DisplayIndex
        {
            [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
            get
            {
                return this.RowIndex;
            }
        }
    }
}
