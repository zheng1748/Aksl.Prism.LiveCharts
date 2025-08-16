using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;

using Prism.Mvvm;

//https://www.cnblogs.com/wzh2010/p/6518834.html

namespace Aksl.Modules.DataGridCustomer.ViewModels
{
    public class EditableCustomerItemViewModel : BindableBase, IDataErrorInfo
    {
        #region Members
        private readonly IDictionary<string, string> _errors;
        #endregion

        #region Constructors
        public EditableCustomerItemViewModel()
        {
            _errors = new Dictionary<string, string>();
        }
        #endregion

        #region Properties
        public bool IsNew => CustomerId == 0;
        #endregion

        #region Editable Properties
        private string[] _customerTypeOptions;
        public string[] CustomerTypeOptions
        {
            get
            {
                if (_customerTypeOptions == null)
                {
                    _customerTypeOptions = new string[]
                    {
                        "(Not Specified)",
                        "Person",
                        "Company"
                    };
                }
                return _customerTypeOptions;
            }
        }

        private string _customerType;
        public string CustomerType
        {
            get => _customerType;
            set
            {
                if (value == _customerType || string.IsNullOrEmpty(value))
                {
                    return;
                }

                if (SetProperty<string>(ref _customerType, value))
                {
                    IsCompany = _customerType == "Company";
                }
            }
        }

        private int _customerId;
        public int CustomerId
        {
            get =>_customerId;
            set => SetProperty(ref _customerId, value);
        }

        private string _firstName;
        [Required(ErrorMessage = "姓不能为空")]
        [StringLength(maximumLength: 32, ErrorMessage = "姓名最大长度为32")]
        public string FirstName
        {
            get => _firstName;
            set => SetProperty<string>(ref _firstName, value);
        }

        private string _lastName;
        [StringLength(maximumLength: 32, ErrorMessage = "姓名最大长度为32")]
        public string LastName
        {
            get => _lastName;
            set => SetProperty<string>(ref _lastName, value);
        }

        private string _email;
        [Required(ErrorMessage = "邮件不能为空")]
        [StringLength(100, MinimumLength = 2)]
        [RegularExpression("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", ErrorMessage = "请填写正确的邮箱地址,例如some@qq.com.")]
        public string Email
        {
            get => _email;
            set => SetProperty<string>(ref _email, value);
        }

        private bool _isCompany;
        public bool IsCompany
        {
            get => _isCompany;
            set => SetProperty(ref _isCompany, value);
        }

        private double _totalSales;
        public double TotalSales
        {
            get => _totalSales;
            set => SetProperty(ref _totalSales, value);
        }

        private bool _validateResult = false;
        public bool ValidateResult
        {
            get => _validateResult;
            set => SetProperty(ref _validateResult, value);
        }

        public bool HasErrors => _errors.Count > 0;
        #endregion

        #region IDataErrorInfo Interface
        public string this[string columnName]
        {
            get
            {
                ValidationContext validationContext = new ValidationContext(this, null, null);
                validationContext.MemberName = columnName;

                var validationResults = new List<ValidationResult>();
                var isValidate = Validator.TryValidateProperty( this.GetType().GetProperty(columnName).GetValue(this, null),  validationContext, validationResults);
                if (!isValidate && validationResults.Any())
                {
                    if (!_errors.ContainsKey(columnName))
                    {
                        _errors.Add(columnName,"");
                       // return _errors[columnName];
                    }

                    return string.Join(Environment.NewLine, validationResults.Select(r => r.ErrorMessage).ToArray());
                }
                else
                {
                    _errors.Remove(columnName);
                }

                RaisePropertyChanged(nameof(HasErrors));

                return null;
            }
            set
            {
                _errors[columnName] = value;
            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
