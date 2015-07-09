using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects;
using System.Data.Metadata.Edm;
using System.Data.Common;

namespace TinyLibrary.Domain
{
    public partial class TinyLibraryContainer
    {

        partial void OnContextCreated()
        {

            this.SavingChanges += (s, e) => FixCascadeDeleteForSingularAssociations();
        }



        public void FixCascadeDeleteForSingularAssociations()
        {

            foreach (var entry in this.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted)

                .Where(e => ((e.IsRelationship == true) && AssociationIsSingular(e))))
            {

                if ((CascadeDelete(entry, 0) && EndIsPresentAndDeleted(entry, 0) && !EndIsPresentAndDeleted(entry, 1))

                    || (CascadeDelete(entry, 1) && EndIsPresentAndDeleted(entry, 1) && !EndIsPresentAndDeleted(entry, 0)))
                {

                    // AcceptChanges on this state entry so the update pipeline ignores it -- depend on the database to do the cascade  

                    entry.AcceptChanges();

                }

            }

        }



        private bool CascadeDelete(ObjectStateEntry entry, int index)
        {

            var dataRecordInfo = ((IExtendedDataRecord)entry.OriginalValues).DataRecordInfo;

            var associationEndMember = dataRecordInfo.FieldMetadata[index].FieldType as AssociationEndMember;

            return (associationEndMember.DeleteBehavior == OperationAction.Cascade);

        }



        private bool EndIsPresentAndDeleted(ObjectStateEntry entry, int index)
        {

            ObjectStateEntry endEntry = null;

            if (!this.ObjectStateManager.TryGetObjectStateEntry(entry.OriginalValues[index], out endEntry))

                return false;

            return (endEntry.State == EntityState.Deleted);

        }



        private bool AssociationIsSingular(ObjectStateEntry entry)
        {

            // IsSingular in this sense means it's 1-to-1 or 1-to-0..1, not many-many or 1-many  

            var dataRecordInfo = ((IExtendedDataRecord)entry.OriginalValues).DataRecordInfo;

            return (EndIsSingular(dataRecordInfo, 0) && EndIsSingular(dataRecordInfo, 1));

        }



        private bool EndIsSingular(DataRecordInfo dataRecordInfo, int index)
        {

            var associationEndMember = dataRecordInfo.FieldMetadata[index].FieldType as AssociationEndMember;

            return (associationEndMember.RelationshipMultiplicity != RelationshipMultiplicity.Many);

        }

    }

}
