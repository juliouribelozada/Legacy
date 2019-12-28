// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatientInfo.cs" company="ATPC.co">
//   © Luis Antonio Soler Barrera
// </copyright>
// <summary>
//   Defines the PatientInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Neuron.HIS.Models.Common
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    using Atpc.Common;

    /// <summary>
    /// Defines PatientInfo class. This Class contain Personal Info Relative to the Patient.
    /// </summary>
    public class PatientInfo : UniqueEntity, IDataErrorInfo
    {
        private string fullName;

        private Gender gender;

        private DateTime? birthDate;

        private string uniqueDocumentId;

        private PoblationGroup poblationGroup;

        private string idDocumentType;

        private string idDocument;

        private string firstName;

        private string middleName;

        private string lastName;

        private string additionalLastName;

        /// <summary>
        /// Raises when the IdDocument Property value Changes.
        /// </summary>
        public event EventHandler IdDocumentChange;

        /// <summary>
        /// Raises when the IdDocumentType Property value Changes.
        /// </summary>
        public event EventHandler IdDocumentTypeChange;

        /// <summary>
        /// Gets or sets IdDocumentType.
        /// </summary>
        public string IdDocumentType
        {
            get
            {
                return this.idDocumentType;
            }

            set
            {
                if (value != this.idDocumentType)
                {
                    this.idDocumentType = value;
                    this.NotifyPropertyChanged("IdDocumentType");
                    this.OnIdDocumentTypeChange();
                }
            }
        }

        /// <summary>
        /// Gets or sets IdDocument.
        /// </summary>
        public string IdDocument
        {
            get
            {
                return this.idDocument;
            }

            set
            {
                if (value != this.idDocument)
                {
                    this.idDocument = value;
                    this.NotifyPropertyChanged("IdDocument");
                    this.OnIdDocumentChange();
                }
            }
        }

        /// <summary>
        /// Gets or sets FirstName.
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                if (value != this.firstName)
                {
                    this.firstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
            }
        }

        /// <summary>
        /// Gets or sets MiddleName.
        /// </summary>
        public string MiddleName
        {
            get
            {
                return this.middleName;
            }

            set
            {
                if (value != this.middleName)
                {
                    this.middleName = value;
                    this.NotifyPropertyChanged("MiddleName");
                }
            }
        }

        
        public object OriginalObject { get; set; }   

        /// <summary>
        /// Gets or sets LastName.
        /// </summary>
        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                if (value != this.lastName)
                {
                    this.lastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }

        /// <summary>
        /// Gets or sets AdditionalLastName.
        /// </summary>
        /// <remarks>In some Countries, The People have 2 Last Names</remarks>
        /// <value>The Second Last Name</value>
        public string AdditionalLastName
        {
            get
            {
                return this.additionalLastName;
            }

            set
            {
                if (value != this.additionalLastName)
                {
                    this.additionalLastName = value;
                    this.NotifyPropertyChanged("AdditionalLastName");
                }
            }
        }

        /// <summary>
        /// Gets or sets FullName.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.fullName;
            }

            set
            {
                if (value != this.fullName)
                {
                    this.fullName = value;
                    this.NotifyPropertyChanged("FullName");
                }
            }
        }

        /// <summary>
        /// Gets or sets Gender.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                if (value != this.gender)
                {
                    this.gender = value;
                    this.NotifyPropertyChanged("Gender");
                }
            }
        }

        public string GenderAsString
        {
            get
            {
                switch (this.Gender)
                {
                    case Gender.NotGiven:
                        return "AMBOS";

                    case Gender.Male:
                        return "MASCULINO";

                    case Gender.Female:
                        return "FEMENINO";

                    default:
                        return "AMBOS";
                }
            }
        }


        /// <summary>
        /// Gets or sets BirthDate.
        /// </summary>
        public DateTime? BirthDate
        {
            get
            {
                return this.birthDate;
            }

            set
            {
                if (this.birthDate != value)
                {
                    this.birthDate = value;
                    this.NotifyPropertyChanged("BirthDate");
                }
            }
        }

        /// <summary>
        /// Gets or sets UniqueDocumentId.
        /// </summary>
        public string UniqueDocumentId
        {
            get
            {
                return this.uniqueDocumentId;
            }

            set
            {
                if (this.uniqueDocumentId != value)
                {
                    this.uniqueDocumentId = value;
                    this.NotifyPropertyChanged("UniqueDocumentId");
                }
            }
        }

        /// <summary>
        /// Gets or sets PoblationGroup.
        /// </summary>
        public PoblationGroup PoblationGroup
        {
            get
            {
                return this.poblationGroup;
            }

            set
            {
                if (this.poblationGroup != value)
                {
                    this.poblationGroup = value;
                    this.NotifyPropertyChanged("PoblationGroup");
                }
            }
        }

        public PatientInfo Copy()
        {
            return (PatientInfo)this.MemberwiseClone();
        }

        public string Error
        {
            get
            {
                return null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Raise the IdDocumentChange Event.
        /// </summary>
        private void OnIdDocumentChange()
        {
            EventHandler handler = this.IdDocumentChange;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        /// <summary>
        /// Raise the IdDocumentTypeChange Event.
        /// </summary>
        private void OnIdDocumentTypeChange()
        {
            EventHandler handler = this.IdDocumentTypeChange;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }
    }
}
