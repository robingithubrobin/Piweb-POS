using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace PiwebSystemsPOS.Classes
{
    class PiwebSystems
    {

        private string connection;
        public string appName = "Piweb Systems POS";
        public PiwebSystems()
        {
            connection = ConfigurationManager.ConnectionStrings["sqlconn"].ConnectionString;
        }

        //
        // Create
        //

        #region Create Company
        /// <summary>
        /// Create Company
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_addressLine1"></param>
        /// <param name="_addressLine2"></param>
        /// <param name="_mobile1"></param>
        /// <param name="_mobile2"></param>
        /// <param name="_faxNo"></param>
        /// <param name="_email"></param>
        /// <param name="_url"></param>
        /// <param name="_location"></param>
        /// <param name="_country"></param>
        /// <param name="phoneNo"></param>
        /// <param name="_VATRegNo"></param>
        /// <param name="_industrialClass"></param>
        /// <param name="_abbreviatedNames"></param>
        /// <param name="_showAbbreviatedNames"></param>
        /// <param name="_isWhatsAppMob1"></param>
        /// <param name="_isWhatsAppMob2"></param>
        /// <param name="_logoPath"></param>
        /// <param name="_createdBy"></param>
        public void CreateCompany(string _name, string _addressLine1, string _addressLine2, string _mobile1, string _mobile2, string _faxNo, string _email, string _url, string _location, string _country, string phoneNo, string _VATRegNo, string _industrialClass, string _abbreviatedNames, char _showAbbreviatedNames, char _isWhatsAppMob1, char _isWhatsAppMob2, string _logoPath, string _createdBy)
        {
            string query = @"INSERT INTO [dbo].[SYS_CompanyInfo]([Name],[AddressLine1],[AddressLine2],[Mobile1],[Mobile2],[FaxNo],[Email],[Url],[Location],[Country],[PhoneNo],[VATRegNo],[IndustrialClass],[AbbreviatedNames],[ShowAbbreviatedNames],[IsWhatsAppMob1],[IsWhatsAppMob2],[logoPath],[CreatedBy],[DateCreated],[ModifiedBy],[DateModified])
                        VALUES ('" + _name + "','" + _addressLine1 + "', '" + _addressLine2 + "', '" + _mobile1 + "', '" + _mobile2 + "','" + _faxNo + "', '" + _email + "', '" + _url + "', '" + _location + "','" + _country + "','" + phoneNo + "', '" + _VATRegNo + "','" + _industrialClass + "', '" + _abbreviatedNames + "','" + _showAbbreviatedNames + "','" + _isWhatsAppMob1 + "', '" + _isWhatsAppMob2 + "','" + _logoPath + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Bank
        /// <summary>
        /// Create Bank
        /// </summary>
        /// <param name="_bankName"></param>
        /// <param name="_branch"></param>
        /// <param name="_accountName"></param>
        /// <param name="_accountNo"></param>
        /// <param name="_companyID"></param>
        public void CreateBank(string _bankName, string _branch, string _accountName, string _accountNo, int _companyID)
        {
            string query = @"INSERT INTO [dbo].[ACC_Bank]([BankName],[Branch],[AccountName],[AccountNo],[COMPID])
                        VALUES ('" + _bankName + "','" + _branch + "','" + _accountName + "','" + _accountNo + "','" + _companyID + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create User
        /// <summary>
        /// Create User
        /// </summary>
        /// <param name="_userName"></param>
        /// <param name="_fullName"></param>
        /// <param name="_state"></param>
        /// <param name="_expiryDate"></param>
        /// <param name="_role"></param>
        /// <param name="_password"></param>
        /// <param name="_email"></param>
        /// <param name="_phone"></param>
        /// <param name="_altPhone"></param>
        /// <param name="_clerkID"></param>
        /// <param name="_clerkName"></param>
        /// <param name="_clerkPassword"></param>
        /// <param name="_useLoginCredentials"></param>
        /// <param name="_printerID"></param>
        /// <param name="_createdBy"></param>
        public void CreateUser(string _userName, string _fullName, char _state, DateTime _expiryDate, int _role, string _password, string _email, string _phone, string _altPhone, int _clerkID, string _clerkName, string _clerkPassword, char _useLoginCredentials, int _printerID, string _createdBy)
        {
            string query = @"INSERT INTO [dbo].[SYS_User]([UserName],[FullName],[State],[ExpiryDate],[Role],[Password],[Email],[Phone],[AltPhone],[ClerkID],[ClerkName],[ClerkPassword],[useLoginCredentials],[PrinterID],[CreatedBy],[DateCreated],[ModifiedBy],[DateModified])
                        VALUES ('" + _userName + "', '" + _fullName + "','" + _state + "', '" + _expiryDate + "','" + _role + "','" + _password + "','" + _email + "','" + _phone + "','" + _altPhone + "','" + _clerkID + "','" + _clerkName + "','" + _clerkPassword + "','" + _useLoginCredentials + "','" + _printerID + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Device
        /// <summary>
        /// Create Device
        /// </summary>
        /// <param name="_model"></param>
        /// <param name="_serialNo"></param>
        /// <param name="_workStation"></param>
        /// <param name="_createdBy"></param>
        public void CreateDevice(string _model, string _serialNo, string _workStation, string _computerName, string _createdBy)
        {
            string query = @"INSERT INTO [dbo].[SYS_Device]([Model],[SerialNo],[WorkStation],[ComputerName],[CreatedBy],[DateCreated],[ModifiedBy],[DateModified])
                        VALUES ('" + _model + "', '" + _serialNo + "', '" + _workStation + "','" + _computerName + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Printer Setting
        public void CreatePrinterSettings(int _deviceID, string _commName, int _timeOut, int _retryCount, int _baundRate, string _systemFlag)
        {
            string query = @"INSERT INTO [dbo].[SYS_PrinterSettings]([DeviceID],[CommName],[timeOut],[RetryCount],[BaundRate],[SystemFlag])
                        VALUES ('" + _deviceID + "', '" + _commName + "', '" + _timeOut + "','" + _retryCount + "','" + _baundRate + "','" + _systemFlag + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Product
        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="_txtNo"></param>
        /// <param name="_txtGTIN"></param>
        /// <param name="_txtProductName"></param>
        /// <param name="_txtDescription"></param>
        /// <param name="_categoryCode"></param>
        /// <param name="_unitOfMeasure"></param>
        /// <param name="_parentCode"></param>
        /// <param name="_brandID"></param>
        /// <param name="_txtUnitPrice"></param>
        /// <param name="_txtUnitPriceExclVAT"></param>
        /// <param name="_priceIncludeVAT"></param>
        /// <param name="_taxgroupCode"></param>
        /// <param name="_discountGroup"></param>
        /// <param name="_salesProduct"></param>
        /// <param name="_active"></param>
        /// <param name="_purchaseProduct"></param>
        /// <param name="_returnAllowed"></param>
        /// <param name="_allowNegStock"></param>
        /// <param name="_photo"></param>
        /// <param name="_createdBy"></param>
        public void CreateProduct(string _txtNo, string _txtGTIN, string _txtProductName, string _txtDescription, string _categoryCode, string _unitOfMeasure, string _parentCode, string _brandID, decimal _costPrice, decimal _txtUnitPrice, decimal _txtUnitPriceExclVAT, char _priceIncludeVAT, string _taxgroupCode, string _discountGroup, char _salesProduct, char _active, char _purchaseProduct, char _returnAllowed, char _allowNegStock, string _photo, string _createdBy)
        {
            string query = @"INSERT INTO [dbo].[INV_Product]([No], [GTIN], [ProductName], [Description], [CategoryCode], [UnitOfMeasureCode], [ParentProductCode], [BrandID], [UnitCost], [UnitPrice], [UnitPriceExclVAT], [PriceIncVAT], [TaxGroupCode], [DiscGroupCode], [SalesProduct], [Active], [PurchaseProduct], [ReturnAllowed], [AllowNegStock], [Photo], [CreatedBy], [CreatedDate],[ModifiedBy], [ModifiedDate])
                        VALUES ('" + _txtNo + "','" + _txtGTIN + "','" + _txtProductName + "','" + _txtDescription + "','" + _categoryCode + "','" + _unitOfMeasure + "','" + _parentCode + "','" + _brandID + "','" + _costPrice + "','" + _txtUnitPrice + "','" + _txtUnitPriceExclVAT + "','" + _priceIncludeVAT + "','" + _taxgroupCode + "','" + _discountGroup + "','" + _salesProduct + "','" + _active + "','" + _purchaseProduct + "','" + _returnAllowed + "','" + _allowNegStock + "','" + _photo + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Create Product without Photo
        /// </summary>
        /// <param name="_txtNo"></param>
        /// <param name="_txtGTIN"></param>
        /// <param name="_txtProductName"></param>
        /// <param name="_txtDescription"></param>
        /// <param name="_categoryCode"></param>
        /// <param name="_unitOfMeasure"></param>
        /// <param name="_parentCode"></param>
        /// <param name="_brandID"></param>
        /// <param name="_costPrice"></param>
        /// <param name="_txtUnitPrice"></param>
        /// <param name="_txtUnitPriceExclVAT"></param>
        /// <param name="_priceIncludeVAT"></param>
        /// <param name="_taxgroupCode"></param>
        /// <param name="_discountGroup"></param>
        /// <param name="_salesProduct"></param>
        /// <param name="_active"></param>
        /// <param name="_purchaseProduct"></param>
        /// <param name="_returnAllowed"></param>
        /// <param name="_allowNegStock"></param>
        /// <param name="_createdBy"></param>
        public void CreateProduct(string _txtNo, string _txtGTIN, string _txtProductName, string _txtDescription, string _categoryCode, string _unitOfMeasure, string _parentCode, string _brandID, decimal _costPrice, decimal _txtUnitPrice, decimal _txtUnitPriceExclVAT, char _priceIncludeVAT, string _taxgroupCode, string _discountGroup, char _salesProduct, char _active, char _purchaseProduct, char _returnAllowed, char _allowNegStock, string _createdBy)
        {
            string query = @"INSERT INTO [dbo].[INV_Product]([No], [GTIN], [ProductName], [Description], [CategoryCode], [UnitOfMeasureCode], [ParentProductCode], [BrandID], [UnitCost], [UnitPrice], [UnitPriceExclVAT], [PriceIncVAT], [TaxGroupCode], [DiscountGroupCode], [SalesProduct], [Active], [PurchaseProduct], [ReturnAllowed], [AllowNegStock], [CreatedBy], [CreatedDate],[ModifiedBy], [ModifiedDate])
                        VALUES ('" + _txtNo + "','" + _txtGTIN + "','" + _txtProductName + "','" + _txtDescription + "','" + _categoryCode + "','" + _unitOfMeasure + "','" + _parentCode + "','" + _brandID + "','" + _costPrice + "','" + _txtUnitPrice + "','" + _txtUnitPriceExclVAT + "','" + _priceIncludeVAT + "','" + _taxgroupCode + "','" + _discountGroup + "','" + _salesProduct + "','" + _active + "','" + _purchaseProduct + "','" + _returnAllowed + "','" + _allowNegStock + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Product Category
        /// <summary>
        /// Create Product Category
        /// </summary>
        /// <param name="_no"></param>
        /// <param name="_parentCategory"></param>
        /// <param name="_description"></param>
        public void CreateProductCategory(string _no, string _parentCategory, string _description, string _discountGroupCode, string _photo)
        {
            string query = @"INSERT INTO [dbo].[INV_ProductCategory]([CategoryCode],[ParentCategory],[Description],[DiscountGroupCode],[Photo]) VALUES('" + _no + "', '" + _parentCategory + "','" + _description + "', '" + _discountGroupCode + "','" + _photo + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Unit Of Measure
        /// <summary>
        /// Create Unit of Measure
        /// </summary>
        /// <param name="_unitOfMeasureCode"></param>
        /// <param name="_unit"></param>
        public void CreateUnitOfMeasure(string _unitOfMeasureCode, string _unit)
        {
            string query = @"INSERT INTO [dbo].[INV_UnitOfMeasure]([UnitOfMeasureCode],[Description]) VALUES('" + _unitOfMeasureCode + "','" + _unit + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Product Brand
        /// <summary>
        /// Create Product Brand
        /// </summary>
        /// <param name="_brand"></param>
        /// <param name="_description"></param>
        /// <param name="_discountGroup"></param>
        public void CreateProductBrand(string _brand, string _description, string _discountGroup)
        {
            string query = @"INSERT INTO [dbo].[INV_ProductBrand]([BrandCode],[Brand],[Description],[DiscountGroup]) VALUES ((Select isnull(MAX([BrandCode]),10000) + 1 from INV_ProductBrand), '" + _brand + "', '" + _description + "', '" + _discountGroup + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Tax Group
        /// <summary>
        /// Create Tax Group
        /// </summary>
        /// <param name="_taxCode"></param>
        /// <param name="_description"></param>
        public void CreateTaxGroup(string _taxCode, string _description)
        {
            string query = @"INSERT INTO [dbo].[COM_TaxGroup] ([TaxGroupCode],[Description]) VALUES ('" + _taxCode + "','" + _description + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Tax Rate
        /// <summary>
        /// Create Tax Rate
        /// </summary>
        /// <param name="_taxGroupCode"></param>
        /// <param name="_description"></param>
        /// <param name="_tax"></param>
        /// <param name="_taxCategory"></param>
        public void CreateTaxRate(string _taxGroupCode, string _description, decimal _tax, string _taxCategory)
        {
            string query = @"INSERT INTO [dbo].[COM_TaxRate]([TaxGroupCode],[Description],[Tax],[TaxCategory]) VALUES ('" + _taxGroupCode + "','" + _description + "', '" + _tax + "', '" + _taxCategory + "')";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Product Adjustment
        /// <summary>
        /// Create Product Adjustment
        /// </summary>
        /// <param name="_serial"></param>
        /// <param name="_productCode"></param>
        /// <param name="_quantity"></param>
        /// <param name="_remarks"></param>
        /// <param name="_createdBy"></param>
        /// <param name="_adjustmentTypeID"></param>
        public void CreateProductAdjustment(string _serial, string _productCode, decimal _quantity, string _remarks, string _createdBy, int _adjustmentTypeID)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = "INSERT INTO [dbo].[INV_Adjustments] ([AdjustmentID],[AdjustmentDate],[ProductCode],[Quantity],[Remarks],[CreatedBy],[CreatedDate],[AdjustmentTypeID]) VALUES ('" + _serial + "',CONVERT(datetime,GETDATE(),103),'" + _productCode + "','" + _quantity + "','" + _remarks + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _adjustmentTypeID + "')";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Inventory Stock
        /// <summary>
        /// Create Inventory Stock
        /// </summary>
        /// <param name="_transactionTypeCode"></param>
        /// <param name="_documentNo"></param>
        /// <param name="_productCode"></param>
        /// <param name="_quantityIn"></param>
        /// <param name="_quantityOut"></param>
        /// <param name="_createdBy"></param>
        public void CreateInventoryStock(string _transactionTypeCode, string _documentNo, string _productCode, decimal _quantityIn, decimal _quantityOut, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = "INSERT INTO [dbo].[INV_InventoryStock]([InventoryDate],[TransactionTypeCode],[DocumentNo],[DocumentDate],[ProductCode],[QuantityIn],[QuantityOut],[CreatedBy],[CreatedDate]) VALUES (CONVERT(datetime,GETDATE(),103),'" + _transactionTypeCode + "','" + _documentNo + "',CONVERT(datetime,GETDATE(),103),'" + _productCode + "','" + _quantityIn + "','" + _quantityOut + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Customer
        /// <summary>
        /// Create Customer
        /// </summary>
        /// <param name="_CustomerCode"></param>
        /// <param name="_name"></param>
        /// <param name="_address"></param>
        /// <param name="_email"></param>
        /// <param name="_city"></param>
        /// <param name="_mobile"></param>
        /// <param name="_altPhoneNo"></param>
        /// <param name="_creditLimit"></param>
        /// <param name="_currencyCode"></param>
        /// <param name="_amount"></param>
        /// <param name="_vatRegistrationNo"></param>
        /// <param name="_photo"></param>
        /// <param name="_county"></param>
        /// <param name="_url"></param>
        /// <param name="_active"></param>
        /// <param name="_createdBy"></param>
        public void CreateCustomer(string _CustomerCode, string _name, string _address, string _email, string _city, string _mobile, string _altPhoneNo, decimal _creditLimit, string _vatRegistrationNo, string _photo, string _url, char _active, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = "INSERT INTO [dbo].[CRM_Customers]([CustomerCode], [Name], [Address], [Email], [City], [PhoneNo], [AltPhoneNo], [CreditLimit], [VATRegistrationNo], [Photo], [Url], [Active], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES ('" + _CustomerCode + "', '" + _name + "','" + _address + "', '" + _email + "', '" + _city + "', '" + _mobile + "', '" + _altPhoneNo + "', '" + _creditLimit + "', '" + _vatRegistrationNo + "', '" + _photo + "', '" + _url + "', '" + _active + "', '" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Create Customer Without Photo
        /// </summary>
        /// <param name="_CustomerCode"></param>
        /// <param name="_name"></param>
        /// <param name="_address"></param>
        /// <param name="_email"></param>
        /// <param name="_city"></param>
        /// <param name="_mobile"></param>
        /// <param name="_altPhoneNo"></param>
        /// <param name="_creditLimit"></param>
        /// <param name="_vatRegistrationNo"></param>
        /// <param name="_url"></param>
        /// <param name="_active"></param>
        /// <param name="_createdBy"></param>
        public void CreateCustomer(string _CustomerCode, string _name, string _address, string _email, string _city, string _mobile, string _altPhoneNo, decimal _creditLimit, string _vatRegistrationNo, string _url, char _active, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = "INSERT INTO [dbo].[CRM_Customers]([Customer Code], [Name], [Address], [Email], [City], [Phone No_], [Alt Phone No_], [Credit Limit], [VAT Registration No_], [Url], [Active], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES ('" + _CustomerCode + "', '" + _name + "','" + _address + "', '" + _email + "', '" + _city + "', '" + _mobile + "', '" + _altPhoneNo + "', '" + _creditLimit + "', '" + _vatRegistrationNo + "', '" + _url + "', '" + _active + "', '" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        #endregion

        #region Create Supplier
        /// <summary>
        /// Create Suppier With Photo
        /// </summary>
        /// <param name="_supplierCode"></param>
        /// <param name="_name"></param>
        /// <param name="_address"></param>
        /// <param name="_email"></param>
        /// <param name="_city"></param>
        /// <param name="_mobile"></param>
        /// <param name="_altPhoneNo"></param>
        /// <param name="_vatRegistrationNo"></param>
        /// <param name="_photo"></param>
        /// <param name="_url"></param>
        /// <param name="_active"></param>
        /// <param name="_createdBy"></param>
        public void CreateSuppier(string _supplierCode, string _name, string _address, string _email, string _city, string _mobile, string _altPhoneNo, string _vatRegistrationNo, string _photo, string _url, char _active, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = "INSERT INTO [dbo].[CRM_Suppliers]([SupplierCode],[Name],[Address],[Email],[City],[PhoneNo],[AltPhoneNo],[VATRegistrationNo],[Photo],[Url],[Active],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _supplierCode + "', '" + _name + "','" + _address + "', '" + _email + "','" + _city + "', '" + _mobile + "', '" + _altPhoneNo + "', '" + _vatRegistrationNo + "', '" + _photo + "', '" + _url + "', '" + _active + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Create Supplier without Photo
        /// </summary>
        /// <param name="_supplierCode"></param>
        /// <param name="_name"></param>
        /// <param name="_address"></param>
        /// <param name="_email"></param>
        /// <param name="_city"></param>
        /// <param name="_mobile"></param>
        /// <param name="_altPhoneNo"></param>
        /// <param name="_vatRegistrationNo"></param>
        /// <param name="_url"></param>
        /// <param name="_active"></param>
        /// <param name="_createdBy"></param>
        public void CreateSuppier(string _supplierCode, string _name, string _address, string _email, string _city, string _mobile, string _altPhoneNo, string _vatRegistrationNo, string _url, char _active, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = "INSERT INTO [dbo].[CRM_Suppliers]([SupplierCode],[Name],[Address],[Email],[City],[PhoneNo],[AltPhoneNo],[VATRegistrationNo],[Url],[Active],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _supplierCode + "', '" + _name + "','" + _address + "', '" + _email + "','" + _city + "', '" + _mobile + "', '" + _altPhoneNo + "', '" + _vatRegistrationNo + "', '" + _url + "', '" + _active + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Purchase Invoice Header
        /// <summary>
        /// Create Purchase Invoice
        /// </summary>
        /// <param name="_purchaseInvoiceNo"></param>
        /// <param name="_receivingDate"></param>
        /// <param name="_statusCode"></param>
        /// <param name="_headerTaxGroupID"></param>
        /// <param name="_POTypeCode"></param>
        /// <param name="_GRN"></param>
        /// <param name="_dueDate"></param>
        /// <param name="_priceListID"></param>
        /// <param name="_subTotal"></param>
        /// <param name="_packagingCharges"></param>
        /// <param name="_deliveryCharges"></param>
        /// <param name="_HeaderStateIDOfSupply"></param>
        /// <param name="_ItemTax"></param>
        /// <param name="_tax1"></param>
        /// <param name="_tax2"></param>
        /// <param name="_tax3"></param>
        /// <param name="_itemDiscount"></param>
        /// <param name="_discountOnTotal"></param>
        /// <param name="_adjustment"></param>
        /// <param name="_rounding"></param>
        /// <param name="_netPayable"></param>
        /// <param name="_taxID1"></param>
        /// <param name="_taxID2"></param>
        /// <param name="_taxID3"></param>
        /// <param name="_taxRate1"></param>
        /// <param name="_taxRate2"></param>
        /// <param name="_taxRate3"></param>
        /// <param name="_totalTaxable"></param>
        /// <param name="_billingAddressSameAsShipping"></param>
        /// <param name="_supplierCode"></param>
        /// <param name="_remarks"></param>
        /// <param name="_supplierReferenceNo"></param>
        /// <param name="_supplierReferenceDate"></param>
        /// <param name="_discountRate"></param>
        /// <param name="_cashDiscount"></param>
        /// <param name="_createdByDeviceID"></param>
        /// <param name="_modifiedByDeviceID"></param>
        /// <param name="_createdBy"></param>
        public void CreatePurchaseInvoice(string _purchaseInvoiceNo, DateTime _receivingDate, string _statusCode, string _headerTaxGroupID, string _POTypeCode, string _GRN, DateTime _dueDate, decimal _priceListID, decimal _subTotal, decimal _packagingCharges, decimal _deliveryCharges, string _HeaderStateIDOfSupply, decimal _ItemTax, decimal _tax1, decimal _tax2, decimal _tax3, decimal _itemDiscount, decimal _discountOnTotal, decimal _adjustment, decimal _rounding, decimal _netPayable, string _taxID1, string _taxID2, string _taxID3, decimal _taxRate1, decimal _taxRate2, decimal _taxRate3, decimal _totalTaxable, char _billingAddressSameAsShipping, string _supplierCode, string _remarks, string _supplierReferenceNo, string _supplierReferenceDate, decimal _discountRate, decimal _cashDiscount, string _createdByDeviceID, string _modifiedByDeviceID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now; //INSERT INTO [dbo].[PUR_PurchaseInvoices]([Purchase Invoice No],[Invoice Date],[Receiving Date],[Status Code],[PO Type Code],[Due Date],[CreatedByDeviceID],[ModifiedByDeviceID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ()
                string query = @"INSERT INTO [dbo].[PUR_PurchaseInvoices]
           ([PurchaseInvoiceNo]
           ,[InvoiceDate]
           ,[ReceivingDate]
           ,[StatusCode]
           ,[HeaderTaxGroupID]
           ,[POTypeCode]
           ,[GRN]
           ,[DueDate]
           ,[PriceListID]
           ,[SubTotal]
           ,[PackagingCharges]
           ,[DeliveryCharges]
           ,[HeaderStateIDOfSupply]
           ,[ItemTax]
           ,[Tax1]
           ,[Tax2]
           ,[Tax3]
           ,[ItemDiscount]
           ,[DiscountOnTotal]
           ,[Adjustment]
           ,[Rounding]
           ,[NetPayable]
           ,[TaxID1]
           ,[TaxID2]
           ,[TaxID3]
           ,[TaxRate1]
           ,[TaxRate2]
           ,[TaxRate3]
           ,[TotalTaxable]
           ,[BillingAddressSameAsShipping]
           ,[SupplierCode]
           ,[Remarks]
           ,[SupplierReferenceNo]
           ,[SupplierReferenceDate]
           ,[DiscountRate]
           ,[CashDiscount]
           ,[CreatedByDeviceID]
           ,[ModifiedByDeviceID]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
          VALUES ('" + _purchaseInvoiceNo + "',CONVERT(datetime,GETDATE(),103),'" + _receivingDate + "','" + _statusCode + "','" + _headerTaxGroupID + "','" + _POTypeCode + "', '" + _GRN + "','" + _dueDate + "','" + _priceListID + "','" + _subTotal + "', '" + _packagingCharges + "', '" + _deliveryCharges + "','" + _HeaderStateIDOfSupply + "','" + _ItemTax + "','" + _tax1 + "','" + _tax2 + "','" + _tax3 + "','" + _itemDiscount + "','" + _discountOnTotal + "','" + _adjustment + "','" + _rounding + "','" + _netPayable + "','" + _taxID1 + "','" + _taxID2 + "','" + _taxID3 + "','" + _taxRate1 + "','" + _taxRate2 + "','" + _taxRate3 + "','" + _totalTaxable + "','" + _billingAddressSameAsShipping + "','" + _supplierCode + "','" + _remarks + "','" + _supplierReferenceNo + "','" + _supplierReferenceDate + "','" + _discountRate + "','" + _cashDiscount + "','" + _createdByDeviceID + "', '" + _modifiedByDeviceID + "','" + _createdBy + "','CONVERT(datetime,GETDATE(),103)','" + _createdBy + "',CONVERT(datetime,GETDATE(),103) )";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Generete Purchase Invoice 
        /// </summary>
        /// <param name="_purchaseInvoiceNo"></param>
        /// <param name="_invoiceDate"></param>
        /// <param name="_receivingDate"></param>
        /// <param name="_statusCode"></param>
        /// <param name="_POTypeCode"></param>
        /// <param name="_dueDate"></param>
        /// <param name="_createdByDeviceID"></param>
        /// <param name="_createdBy"></param>
        public void CreatePurchaseInvoice(string _purchaseInvoiceNo, DateTime _invoiceDate, DateTime _receivingDate, string _statusCode, string _POTypeCode, DateTime _dueDate, string _supplierCode, string _supplierRefNo, string _createdByDeviceID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[PUR_PurchaseInvoices]([PurchaseInvoiceNo],[InvoiceDate],[ReceivingDate],[StatusCode],[POTypeCode],[DueDate],[SupplierCode],[SupplierReferenceNo],[CreatedByDeviceID],[ModifiedByDeviceID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _purchaseInvoiceNo + "','" + _invoiceDate + "','" + _receivingDate + "','" + _statusCode + "','" + _POTypeCode + "','" + _receivingDate + "','" + _supplierCode + "', '" + _supplierRefNo + "','" + _createdByDeviceID + "','" + _createdByDeviceID + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103) )";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Purchase Invoice Line
        /// <summary>
        /// Create Purchase Invoice Lines
        /// </summary>
        /// <param name="_purchaseInvoiceLineID"></param>
        /// <param name="_purchaseInvoiceNo"></param>
        /// <param name="_productCode"></param>
        /// <param name="_description"></param>
        /// <param name="_unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="_unitOfMeasureCode"></param>
        /// <param name="_linePrice"></param>
        /// <param name="_lineDiscount"></param>
        /// <param name="_extendedPrice"></param>
        /// <param name="_statusCode"></param>
        /// <param name="_tax1ID"></param>
        /// <param name="tax2ID"></param>
        /// <param name="tax3ID"></param>
        /// <param name="_lineTax1"></param>
        /// <param name="lineTax2"></param>
        /// <param name="_lineTax3"></param>
        /// <param name="_tax1Rate"></param>
        /// <param name="tax2Rate"></param>
        /// <param name="tax3Rate"></param>
        /// <param name="_taxInclusive"></param>
        /// <param name="_lineTaxGroupID"></param>
        /// <param name="_fixedPrice"></param>
        /// <param name="_discountRate"></param>
        /// <param name="_cashDiscount"></param>
        /// <param name="_remarks"></param>
        /// <param name="_createdBy"></param>
        public void CreatePurchaseInvoiceLines(string _purchaseInvoiceLineID, string _purchaseInvoiceNo, string _productCode, string _description, decimal _unitPrice, int quantity, string _unitOfMeasureCode, decimal _linePrice, decimal _lineDiscount, decimal _extendedPrice, string _statusCode, string _tax1ID, string tax2ID, string tax3ID, decimal _lineTax1, decimal lineTax2, decimal _lineTax3, decimal _tax1Rate, decimal tax2Rate, decimal tax3Rate, char _taxInclusive, string _lineTaxGroupID, decimal _fixedPrice, decimal _discountRate, decimal _cashDiscount, string _remarks, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[PUR_PurchaseInvoiceLines]([PurchaseInvoiceLineID],[PurchaseInvoiceNo],[ProductCode],[Description],[UnitPrice],[Quantity],[UnitofMeasureCode],[LinePrice],[LineDiscount],[ExtendedPrice],[StatusCode],[Tax1ID],[Tax2ID],[Tax3ID],[LineTax1],[LineTax2],[LineTax3],[Tax1Rate],[Tax2Rate],[Tax3Rate],[TaxInclusive],[LineTaxGroupID],[FixedPrice],[DiscountRate],[CashDiscount],[Remarks],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _purchaseInvoiceLineID + "','" + _purchaseInvoiceNo + "', '" + _productCode + "','" + _description + "', '" + _unitPrice + "', '" + quantity + "', '" + _unitOfMeasureCode + "', '" + _linePrice + "', '" + _lineDiscount + "', '" + _extendedPrice + "','" + _statusCode + "', '" + _tax1ID + "', '" + tax2ID + "', '" + tax3ID + "', '" + _lineTax1 + "', '" + lineTax2 + "', '" + _lineTax3 + "', '" + _tax1Rate + "', '" + tax2Rate + "', '" + tax3Rate + "', '" + _taxInclusive + "', '" + _lineTaxGroupID + "', '" + _fixedPrice + "', '" + _discountRate + "', '" + _cashDiscount + "', '" + _remarks + "', '" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Sales Invoice Header
        public void CreateSalesInvoice(string _salesInvoiceNo, DateTime _invoiceDate, string _statusCode, decimal _subTotal, string _createdByDeviceID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[SAL_SalesInvoices]([SalesInvoiceNo],[InvoiceDate],[StatusCode],[SubTotal],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[CreatedByDeviceID],[ModifiedByDeviceID]) VALUES ('" + _salesInvoiceNo + "', '" + _invoiceDate + "','" + _statusCode + "','" + _subTotal + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdByDeviceID + "','" + _createdByDeviceID + "' )";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Sales Invoice Line
        public void CreateSalesInvoiceLine(string _salesInvoiceNo, string _productCode, string _description, decimal _quantity, string _unitCode, string _statusCode, string _priceListID, decimal _unitPrice, decimal _linePrice, string _tax1ID, decimal _lineTax1, decimal _tax1Rate, string _lineTaxGroupID, string _discountRuleID, decimal _lineDiscount, decimal _extendedPrice, char _taxInclusive, decimal _fixedPrice, decimal discountRate, decimal _cashDiscount, string _createdBy, string _createdByDeviceID)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[SAL_SalesInvoiceLines] ([SalesInvoiceNo], [ProductCode], [Description], [Quantity], [UnitCode], [StatusCode], [PriceListID], [UnitPrice], [LinePrice], [Tax1ID], [LineTax1], [Tax1Rate], [LineTaxGroupID], [DiscountRuleID], [LineDiscount], [ExtendedPrice], [TaxInclusive], [FixedPrice], [DiscountRate], [CashDiscount], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [CreatedByDeviceID], [ModifiedByDeviceID]) VALUES ('" + _salesInvoiceNo + "','" + _productCode + "', '" + _description + "', '" + _quantity + "', '" + _unitCode + "', '" + _statusCode + "', '" + _priceListID + "', '" + _unitPrice + "', '" + _linePrice + "', '" + _tax1ID + "', '" + _lineTax1 + "', '" + _tax1Rate + "', '" + _lineTaxGroupID + "','" + _discountRuleID + "', '" + _lineDiscount + "', '" + _extendedPrice + "', '" + _taxInclusive + "', '" + _fixedPrice + "','" + discountRate + "','" + _cashDiscount + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdByDeviceID + "','" + _createdByDeviceID + "' )";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Payment Lines
        //CreatePaymentLine(DateTime _transactionDate,string _transactionTypeCode, string _voucherNo, decimal _transactionAmount, decimal _tenderedAmount, string _paymentModeCode, int _cardID, int _chequeID, int _giftVoucherID, string _createdBy)
        //INSERT INTO [dbo].[ACC_PaymentLines]([TransactionDate],[TransactionTypeCode],[VoucherNo],[TransactionAmount],[TenderedAmount],[PaymentModeCode],[CardID],[ChequeID],[GiftVoucherID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES (_transactionDate,_transactionTypeCode,_voucherNo,_transactionAmount,_tenderedAmount,_paymentModeCode,_cardID,_chequeID,_giftVoucherID,_createdBy,_createdDate,_modifiedBy,_modifiedDate)

        /// <summary>
        /// Create Payment By Cash
        /// </summary>
        /// <param name="_transactionTypeCode"></param>
        /// <param name="_voucherNo"></param>
        /// <param name="_transactionAmount"></param>
        /// <param name="_tenderedAmount"></param>
        /// <param name="_paymentModeCode"></param>
        /// <param name="_createdBy"></param>
        public void CreatePaymentLine(string _transactionTypeCode, string _voucherNo, decimal _transactionAmount, decimal _tenderedAmount, string _paymentModeCode, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[ACC_PaymentLines]([TransactionDate],[TransactionTypeCode],[VoucherNo],[TransactionAmount],[TenderedAmount],[PaymentModeCode],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
                                    VALUES (CONVERT(datetime,GETDATE(),103),'" + _transactionTypeCode + "','" + _voucherNo + "','" + _transactionAmount + "','" + _tenderedAmount + "','" + _paymentModeCode + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Create Payment By Card
        /// </summary>
        /// <param name="_transactionTypeCode"></param>
        /// <param name="_voucherNo"></param>
        /// <param name="_transactionAmount"></param>
        /// <param name="_paymentModeCode"></param>
        /// <param name="_cardID"></param>
        /// <param name="_createdBy"></param>
        public void CreatePaymentLineCard(string _transactionTypeCode, string _voucherNo, decimal _transactionAmount, string _paymentModeCode, int _cardID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[ACC_PaymentLines]([TransactionDate],[TransactionTypeCode],[VoucherNo],[TransactionAmount],[TenderedAmount],[PaymentModeCode],[CardID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
                                    VALUES (CONVERT(datetime,GETDATE(),103),'" + _transactionTypeCode + "','" + _voucherNo + "','" + _transactionAmount + "','" + _transactionAmount + "','" + _paymentModeCode + "','" + _cardID + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Create Payment By Cheque
        /// </summary>
        /// <param name="_transactionTypeCode"></param>
        /// <param name="_voucherNo"></param>
        /// <param name="_transactionAmount"></param>
        /// <param name="_paymentModeCode"></param>
        /// <param name="_chequeID"></param>
        /// <param name="_createdBy"></param>
        ///                                                                 payMode, invoiceNo, totalAmount, payMode, paymentTypeMode,1, username
        public void CreatePaymentLineCheque(string _transactionTypeCode, string _voucherNo, decimal _transactionAmount, string _paymentModeCode, int _chequeID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[ACC_PaymentLines]([TransactionDate],[TransactionTypeCode],[VoucherNo],[TransactionAmount],[TenderedAmount],[PaymentModeCode],[ChequeID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
                                    VALUES (CONVERT(datetime,GETDATE(),103),'" + _transactionTypeCode + "','" + _voucherNo + "','" + _transactionAmount + "','" + _transactionAmount + "','" + _paymentModeCode + "','" + _chequeID + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Create Payment By GiftVoucher
        /// </summary>
        /// <param name="_transactionTypeCode"></param>
        /// <param name="_voucherNo"></param>
        /// <param name="_transactionAmount"></param>
        /// <param name="_paymentModeCode"></param>
        /// <param name="_giftVoucherID"></param>
        /// <param name="_createdBy"></param>
        public void CreatePaymentLineGiftVoucher(string _transactionTypeCode, string _voucherNo, decimal _transactionAmount, string _paymentModeCode, int _giftVoucherID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[ACC_PaymentLines]([TransactionDate],[TransactionTypeCode],[VoucherNo],[TransactionAmount],[TenderedAmount],[PaymentModeCode],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
                                    VALUES (CONVERT(datetime,GETDATE(),103),'" + _transactionTypeCode + "','" + _voucherNo + "','" + _transactionAmount + "','" + _paymentModeCode + "','" + _giftVoucherID + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Card Details
        /// <summary>
        /// Create Card Details
        /// </summary>
        /// <param name="_transactionReferenceID"></param>
        /// <param name="_cardNo"></param>
        /// <param name="_cardExpiryDate"></param>
        /// <param name="_cardTransactionNo"></param>
        /// <param name="_amount"></param>
        /// <param name="_createdBy"></param>

        public void CreateCardDetails(string _transactionReferenceID, string _bankName, int _cardNo, DateTime _cardExpiryDate, string _cardTransactionNo, decimal _amount, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"INSERT INTO [dbo].[ACC_PaymentByCard] ([TransactionReferenceID],[BankName],[CardNo],[CardExpiryDate],[Amount],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
                                    VALUES ('" + _transactionReferenceID + "', '" + _bankName + "' ,'" + _cardNo + "', '" + _cardExpiryDate + "','" + _amount + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Cheque Details

        /// <summary>
        /// Create Cheque Details
        /// </summary>
        /// <param name="_transactionReferenceID"></param>
        /// <param name="_bankAccountNo"></param>
        /// <param name="bankName"></param>
        /// <param name="_branchName"></param>
        /// <param name="_chequeNo"></param>
        /// <param name="_chequeDate"></param>
        /// <param name="_amount"></param>
        /// <param name="_createdBy"></param>
        public void CreateChequeDetails(string _transactionReferenceID, string _bankAccountNo, string bankName, string _branchName, string _chequeNo, DateTime _chequeDate, decimal _amount, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[ACC_PaymentByCheque] ([TransactionReferenceID],[BankAccountNo],[BankName],[BranchName],[ChequeNo],[ChequeDate],[Amount],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])
                                    VALUES ('" + _transactionReferenceID + "', '" + _bankAccountNo + "', '" + bankName + "', '" + _branchName + "', '" + _chequeNo + "','" + _chequeDate + "','" + _amount + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create SalesQuote Header
        public void CreateSalesQuoteHeader(string _salesQuoteNo, string _statusCode, DateTime _salesQuoteDate, DateTime _salesQuoteEndDate, string _SOType, string _customerCode, string _customerName, string _customerPhoneNo, string _createdBy, string _createdByDeviceID)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[SAL_SalesQuotes]([SalesQuoteNo],[StatusCode],[SalesQuoteDate],[SalesQuoteEndDate],[SOType],[CustomerCode],[CustomerName],[CustomerPhoneNo],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[CreatedByDeviceID], [ModifiedByDeviceID])
                                    VALUES ('" + _salesQuoteNo + "','" + _statusCode + "','" + _salesQuoteDate + "', '" + _salesQuoteEndDate + "','" + _SOType + "','" + _customerCode + "','" + _customerName + "','" + _customerPhoneNo + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdByDeviceID + "','" + _createdByDeviceID + "')";

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Sales Quote Line
        /// <summary>
        /// Create Sales Quote Line
        /// </summary>
        /// <param name="_salesQuoteLineID"></param>
        /// <param name="_salesQuoteNo"></param>
        /// <param name="_productCode"></param>
        /// <param name="_description"></param>
        /// <param name="_unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="_unitOfMeasureCode"></param>
        /// <param name="_linePrice"></param>
        /// <param name="_lineDiscount"></param>
        /// <param name="_extendedPrice"></param>
        /// <param name="_statusCode"></param>
        /// <param name="_tax1ID"></param>
        /// <param name="tax2ID"></param>
        /// <param name="tax3ID"></param>
        /// <param name="_lineTax1"></param>
        /// <param name="lineTax2"></param>
        /// <param name="_lineTax3"></param>
        /// <param name="_tax1Rate"></param>
        /// <param name="tax2Rate"></param>
        /// <param name="tax3Rate"></param>
        /// <param name="_taxInclusive"></param>
        /// <param name="_lineTaxGroupID"></param>
        /// <param name="_fixedPrice"></param>
        /// <param name="_discountRate"></param>
        /// <param name="_cashDiscount"></param>
        /// <param name="_remarks"></param>
        /// <param name="_createdBy"></param>
        public void CreateSalesQuoteLines(string _salesQuoteLineID, string _salesQuoteNo, string _productCode, string _description, decimal _unitPrice, int quantity, string _unitOfMeasureCode, decimal _linePrice, decimal _lineDiscount, decimal _extendedPrice, string _statusCode, string _tax1ID, string tax2ID, string tax3ID, decimal _lineTax1, decimal lineTax2, decimal _lineTax3, decimal _tax1Rate, decimal tax2Rate, decimal tax3Rate, char _taxInclusive, string _lineTaxGroupID, decimal _fixedPrice, decimal _discountRate, decimal _cashDiscount, string _remarks, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[SAL_SalesQuoteLines]([SalesQuoteLineID],[SalesQuoteNo],[ProductCode],[Description],[UnitPrice],[Quantity],[UnitofMeasureCode],[LinePrice],[LineDiscount],[ExtendedPrice],[StatusCode],[Tax1ID],[Tax2ID],[Tax3ID],[LineTax1],[LineTax2],[LineTax3],[Tax1Rate],[Tax2Rate],[Tax3Rate],[TaxInclusive],[LineTaxGroupID],[FixedPrice],[DiscountRate],[CashDiscount],[Remarks],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _salesQuoteLineID + "','" + _salesQuoteNo + "', '" + _productCode + "','" + _description + "', '" + _unitPrice + "', '" + quantity + "', '" + _unitOfMeasureCode + "', '" + _linePrice + "', '" + _lineDiscount + "', '" + _extendedPrice + "','" + _statusCode + "', '" + _tax1ID + "', '" + tax2ID + "', '" + tax3ID + "', '" + _lineTax1 + "', '" + lineTax2 + "', '" + _lineTax3 + "', '" + _tax1Rate + "', '" + tax2Rate + "', '" + tax3Rate + "', '" + _taxInclusive + "', '" + _lineTaxGroupID + "', '" + _fixedPrice + "', '" + _discountRate + "', '" + _cashDiscount + "', '" + _remarks + "', '" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Sales Order
        public void CreateSalesOrder(string _salesOrderNo, DateTime _orderDate, string _SOTypeCode, string _customerID, string _statusCode, string _deliveryAddress, string _deliveryArea, string _deliveryZipCode, string _referenceNo, DateTime _deliveryTime, decimal _packagingCharges, decimal _deliveryCharges, DateTime _dueDate, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[SAL_SalesOrders] ([SalesOrderNo],[OrderDate],[SOTypeCode],[CustomerID],[StatusCode],[DeliveryAddress],[DeliveryArea] ,[DeliveryZipCode],[QuotationNo] ,[DeliveryTime],[PackagingCharges],[DeliveryCharges],[DueDate],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _salesOrderNo + "', '" + _orderDate + "','" + _SOTypeCode + "', '" + _customerID + "','" + _statusCode + "', '" + _deliveryAddress + "', '" + _deliveryArea + "' , '" + _deliveryZipCode + "', '" + _referenceNo + "' ,'" + _deliveryTime + "','" + _packagingCharges + "','" + _deliveryCharges + "','" + _dueDate + "','" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create Sales Order Lines
        public void CreateSalesOrderLines(string _salesOrderLineID, string _salesOrderNo, string _productCode, string _description, decimal _unitPrice, int quantity, string _unitOfMeasureCode, decimal _linePrice, decimal _lineDiscount, decimal _extendedPrice, string _statusCode, string _tax1ID, string tax2ID, string tax3ID, decimal _lineTax1, decimal lineTax2, decimal _lineTax3, decimal _tax1Rate, decimal tax2Rate, decimal tax3Rate, char _taxInclusive, string _lineTaxGroupID, decimal _fixedPrice, decimal _discountRate, decimal _cashDiscount, string _remarks, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[SAL_SalesOrderLines]([SalesOrderLineID],[SalesOrderNo],[ProductCode],[Description],[Quantity],[UnitCode],[StatusCode],[UnitPrice],[LinePrice],[LineTaxGroupID],[Tax1ID],[LineTax1],[Tax1Rate],[DiscountRuleID],[TaxInclusive],[FixedPrice],[CashDiscount],[DiscountRate],[LineDiscount],[ExtendedPrice],[ParentLineID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _salesOrderLineID + "','" + _salesOrderNo + "', '" + _productCode + "','" + _description + "', '" + _unitPrice + "', '" + quantity + "', '" + _unitOfMeasureCode + "', '" + _linePrice + "', '" + _lineDiscount + "', '" + _extendedPrice + "','" + _statusCode + "', '" + _tax1ID + "', '" + tax2ID + "', '" + tax3ID + "', '" + _lineTax1 + "', '" + lineTax2 + "', '" + _lineTax3 + "', '" + _tax1Rate + "', '" + tax2Rate + "', '" + tax3Rate + "', '" + _taxInclusive + "', '" + _lineTaxGroupID + "', '" + _fixedPrice + "', '" + _discountRate + "', '" + _cashDiscount + "', '" + _remarks + "', '" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create report Summary
        //public void CreateSalesOrderLines(string _salesOrderLineID, string _salesOrderNo, string _productCode, string _description, decimal _unitPrice, int quantity, string _unitOfMeasureCode, decimal _linePrice, decimal _lineDiscount, decimal _extendedPrice, string _statusCode, string _tax1ID, string tax2ID, string tax3ID, decimal _lineTax1, decimal lineTax2, decimal _lineTax3, decimal _tax1Rate, decimal tax2Rate, decimal tax3Rate, char _taxInclusive, string _lineTaxGroupID, decimal _fixedPrice, decimal _discountRate, decimal _cashDiscount, string _remarks, string _createdBy)
        //{
        //    using (SqlConnection cn = new SqlConnection(connection))
        //    {
        //        string query = @"INSERT INTO [dbo].[SAL_SalesOrderLines]([SalesOrderLineID],[SalesOrderNo],[ProductCode],[Description],[Quantity],[UnitCode],[StatusCode],[UnitPrice],[LinePrice],[LineTaxGroupID],[Tax1ID],[LineTax1],[Tax1Rate],[DiscountRuleID],[TaxInclusive],[FixedPrice],[CashDiscount],[DiscountRate],[LineDiscount],[ExtendedPrice],[ParentLineID],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) VALUES ('" + _salesOrderLineID + "','" + _salesOrderNo + "', '" + _productCode + "','" + _description + "', '" + _unitPrice + "', '" + quantity + "', '" + _unitOfMeasureCode + "', '" + _linePrice + "', '" + _lineDiscount + "', '" + _extendedPrice + "','" + _statusCode + "', '" + _tax1ID + "', '" + tax2ID + "', '" + tax3ID + "', '" + _lineTax1 + "', '" + lineTax2 + "', '" + _lineTax3 + "', '" + _tax1Rate + "', '" + tax2Rate + "', '" + tax3Rate + "', '" + _taxInclusive + "', '" + _lineTaxGroupID + "', '" + _fixedPrice + "', '" + _discountRate + "', '" + _cashDiscount + "', '" + _remarks + "', '" + _createdBy + "',CONVERT(datetime,GETDATE(),103),'" + _createdBy + "',CONVERT(datetime,GETDATE(),103))";
        //        using (SqlCommand cmd = new SqlCommand(query, cn))
        //        {
        //            cn.Open();
        //            cmd.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //    }
        //}
        #endregion

        #region Create No Series Entry
        public void CreateNoSeriesEntry(string code, string description, int defaultNos, int manualNos, int dateOrder)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[No_ Series]([Code], [Description], [DefaultNos], [ManualNos], [DateOrder]) VALUES ('"+code+"', '"+description+"', "+defaultNos+", "+manualNos+", 0)";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Create No Series Details
        public void CreateNoSeriesDetails(string code, DateTime startDate, string startingNo, string endingNo, string warningNo, int incrementNo, string lastNoUsed, int isOpen, DateTime lastDateUsed)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string query = @"INSERT INTO [dbo].[No_ Series Line]([SeriesCode], [StartingDate], [StartingNo], [EndingNo], [WarningNo], [IncrementByNo], [LastNoUsed], [isOpen], [LastDateUsed]) VALUES ('" + code + "', '" + startDate + "', '" + startingNo + "', '" + endingNo + "', '" + warningNo + "', '" + incrementNo + "', '" + lastNoUsed + "', " + isOpen + ", '" + lastDateUsed + "')";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        //
        // Update
        //
        #region Update Sales Invoice Header
        public void UpdateSalesInvoice(string _salesInvoiceNo, string _statusCode, decimal _subTotal, decimal _tax, decimal _discount, string _createdByDeviceID, string _createdBy)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"UPDATE [dbo].[SAL_SalesInvoices] SET [StatusCode] = '" + _statusCode + "', [SubTotal]=" + _subTotal + ",[ItemTax]= " + _tax + ",[ItemDiscount] = " + _discount + ",[CreatedBy]='" + _createdBy + "',[CreatedDate]=CONVERT(datetime,GETDATE(),103),[ModifiedBy]='" + _createdBy + "',[ModifiedDate]=CONVERT(datetime,GETDATE(),103),[CreatedByDeviceID]='" + _createdByDeviceID + "',[ModifiedByDeviceID]='" + _createdByDeviceID + "' WHERE  [SalesInvoiceNo] = '" + _salesInvoiceNo + "'";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update Sales Invoice Line
        public void UpdateSalesInvoiceLine(string _salesInvoiceNo, string _productCode, string _description, decimal _quantity, string _unitCode, string _statusCode, string _priceListID, decimal _unitPrice, decimal _linePrice, string _tax1ID, decimal _lineTax1, decimal _tax1Rate, string _lineTaxGroupID, string _discountRuleID, decimal _lineDiscount, decimal _extendedPrice, char _taxInclusive, decimal _fixedPrice, decimal discountRate, decimal _cashDiscount, string _createdBy, string _createdByDeviceID)
        {
            using (SqlConnection cn = new SqlConnection(connection))
            {
                DateTime datecreated = DateTime.Now;
                string query = @"UPDATE [dbo].[SAL_SalesInvoiceLines] SET [ProductCode] ='" + _productCode + "', [Description] = '" + _description + "', [Quantity] = '" + _quantity + "', [UnitCode] = '" + _unitCode + "', [StatusCode] = '" + _statusCode + "', [PriceListID]= '" + _priceListID + "', [UnitPrice] = '" + _unitPrice + "', [LinePrice] = '" + _linePrice + "', [Tax1ID] = '" + _tax1ID + "', [LineTax1] = '" + _lineTax1 + "', [Tax1Rate] = '" + _tax1Rate + "', [LineTaxGroupID] = '" + _lineTaxGroupID + "', [DiscountRuleID] = '" + _discountRuleID + "', [LineDiscount] = '" + _lineDiscount + "', [ExtendedPrice] = '" + _extendedPrice + "', [TaxInclusive] = '" + _taxInclusive + "', [FixedPrice] = '" + _fixedPrice + "', [DiscountRate] = '" + discountRate + "', [CashDiscount] = '" + _cashDiscount + "', [CreatedBy] = '" + _createdBy + "', [CreatedDate] = 'CONVERT(datetime,GETDATE(),103)', [ModifiedBy]='" + _createdBy + "', [ModifiedDate] = 'CONVERT(datetime,GETDATE(),103)', [CreatedByDeviceID] = '" + _createdByDeviceID + "', [ModifiedByDeviceID] = '" + _createdByDeviceID + "' WHERE [SalesInvoiceNo] = '" + _salesInvoiceNo + "'";
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion
        #region Update Company Info
        public void UpdateCompanyInfo(int _compId, string _name, string _addressLine1, string _addressLine2, string _mobile1, string _mobile2, string _faxNo, string _email, string _url, string _location, string _country, string phoneNo, string _VATRegNo, string _industrialClass, string _abbreviatedNames, char _showAbbreviatedNames, char _isWhatsAppMob1, char _isWhatsAppMob2, string _logoPath, string _createdBy)
        {
            string query = @"UPDATE [dbo].[SYS_CompanyInfo] SET [Name] = '" + _name + "',[AddressLine1] = '" + _addressLine1 + "',[AddressLine2] = '" + _addressLine2 + "',[Mobile1] = '" + _mobile1 + "',[Mobile2] = '" + _mobile2 + "',[FaxNo] = '" + _faxNo + "',[Email] = '" + _email + "',[Url] = '" + _url + "',[Location] = '" + _location + "',[Country] = '" + _country + "',[PhoneNo] = '" + phoneNo + "',[VATRegNo] = '" + _VATRegNo + "',[IndustrialClass] = '" + _industrialClass + "',[AbbreviatedNames] = '" + _abbreviatedNames + "',[ShowAbbreviatedNames] = '" + _showAbbreviatedNames + "',[IsWhatsAppMob1] = '" + _isWhatsAppMob1 + "',[IsWhatsAppMob2] = '" + _isWhatsAppMob2 + "',[logoPath] = '" + _logoPath + "',[ModifiedBy] = '" + _createdBy + "',[DateModified] = CONVERT(datetime,GETDATE(),103) WHERE COMPID = '" + _compId + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update Bank
        public void UpdateBank(int _compID, string _bankName, string _branch, string _accountName, string _accountNo)
        {
            string query = @"UPDATE [dbo].[ACC_Bank] SET [BankName] = '" + _bankName + "',[Branch] = '" + _branch + "',[AccountName] = '" + _accountName + "',[AccountNo] = '" + _accountNo + "' WHERE COMPID = '" + _compID + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update User
        public void UpdateUser(int _userID, string _userName, string _fullName, char _state, DateTime _expiryDate, int _role, string _password, string _email, string _phone, string _altPhone, int _clerkID, string _clerkName, string _clerkPassword, char _useLoginCredentials, int _printerID, string _createdBy)
        {
            string query = @"UPDATE [dbo].[SYS_User] SET [UserName] = '" + _userName + "',[FullName] = '" + _fullName + "',[State] = '" + _state + "',[ExpiryDate] = '" + _expiryDate + "',[Role] = '" + _role + "',[Password] = '" + _password + "',[Email] = '" + _email + "',[Phone] = '" + _phone + "',[AltPhone] = '" + _altPhone + "',[ClerkID] = '" + _clerkID + "',[ClerkName] = '" + _clerkName + "',[ClerkPassword] = '" + _clerkPassword + "',[useLoginCredentials] = '" + _useLoginCredentials + "',[PrinterID] = '" + _printerID + "',[ModifiedBy] = '" + _createdBy + "',[DateModified] = CONVERT(datetime,GETDATE(),103) WHERE UserID = '" + _userID + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update Printer Settings
        public void UpdatePrinterSettings(int _deviceID, string _commName, int _timeOut, int _retryCount, int _baundRate, int _systemFlag)
        {
            string query = @"UPDATE [dbo].[SYS_PrinterSettings] SET [CommName] = '" + _commName + "',[timeOut] = '" + _timeOut + "',[RetryCount] = '" + _retryCount + "',[BaundRate] = '" + _baundRate + "',[SystemFlag] = '" + _systemFlag + "' WHERE [DeviceID] = '" + _deviceID + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update Device
        /// <summary>
        /// Update Device
        /// </summary>
        /// <param name="_deviceID"></param>
        /// <param name="_model"></param>
        /// <param name="_serialNo"></param>
        /// <param name="_workStation"></param>
        /// <param name="_createdBy"></param>
        public void UpdateDevice(int _deviceID, string _model, string _serialNo, string _workStation, string _createdBy)
        {
            string query = @"UPDATE [dbo].[SYS_Device] SET [Model] = '" + _model + "',[SerialNo] = '" + _serialNo + "',[WorkStation] = '" + _workStation + "',[ModifiedBy] = '" + _createdBy + "',[DateModified] = CONVERT(datetime,GETDATE(),103) WHERE DeviceID = '" + _deviceID + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region UpdateProduct
        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="_txtNo"></param>
        /// <param name="_txtGTIN"></param>
        /// <param name="_txtProductName"></param>
        /// <param name="_txtDescription"></param>
        /// <param name="_categoryCode"></param>
        /// <param name="_unitOfMeasure"></param>
        /// <param name="_parentCode"></param>
        /// <param name="_brandID"></param>
        /// <param name="_costPrice"></param>
        /// <param name="_txtUnitPrice"></param>
        /// <param name="_txtUnitPriceExclVAT"></param>
        /// <param name="_priceIncludeVAT"></param>
        /// <param name="_taxgroupCode"></param>
        /// <param name="_discountGroup"></param>
        /// <param name="_salesProduct"></param>
        /// <param name="_active"></param>
        /// <param name="_purchaseProduct"></param>
        /// <param name="_returnAllowed"></param>
        /// <param name="_allowNegStock"></param>
        /// <param name="_photo"></param>
        /// <param name="_createdBy"></param>
        public void UpdateProduct(string _txtNo, string _txtGTIN, string _txtProductName, string _txtDescription, string _categoryCode, string _unitOfMeasure, string _parentCode, string _brandID, decimal _costPrice, decimal _txtUnitPrice, decimal _txtUnitPriceExclVAT, char _priceIncludeVAT, string _taxgroupCode, string _discountGroup, char _salesProduct, char _active, char _purchaseProduct, char _returnAllowed, char _allowNegStock, string _photo, string _createdBy)
        {
            string query = @"UPDATE [dbo].[INV_Product] SET [GTIN] = '" + _txtGTIN + "', [ProductName] = '" + _txtProductName + "', [Description] = '" + _txtDescription + "', [CategoryCode] = '" + _categoryCode + "', [UnitOfMeasureCode] = '" + _unitOfMeasure + "', [ParentProductCode] = '" + _parentCode + "', [BrandID] = '" + _brandID + "', [UnitCost] = '" + _costPrice + "', [UnitPrice] = '" + _txtUnitPrice + "', [UnitPriceExclVAT] = '" + _txtUnitPriceExclVAT + "', [PriceIncVAT] = '" + _priceIncludeVAT + "', [TaxGroupCode] = '" + _taxgroupCode + "', [DiscGroupCode] = '" + _discountGroup + "', [SalesProduct] = '" + _salesProduct + "', [Active] = '" + _active + "', [PurchaseProduct] = '" + _purchaseProduct + "', [ReturnAllowed] = '" + _returnAllowed + "', [AllowNegStock] = '" + _allowNegStock + "', [Photo] = '" + _photo + "', [ModifiedBy] = '" + _createdBy + "', [ModifiedDate] = CONVERT(datetime,GETDATE(),103) WHERE [No] = '" + _txtNo + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }

        /// <summary>
        /// Update Products Exluding Photo
        /// </summary>
        /// <param name="_txtNo"></param>
        /// <param name="_txtGTIN"></param>
        /// <param name="_txtProductName"></param>
        /// <param name="_txtDescription"></param>
        /// <param name="_categoryCode"></param>
        /// <param name="_unitOfMeasure"></param>
        /// <param name="_parentCode"></param>
        /// <param name="_brandID"></param>
        /// <param name="_costPrice"></param>
        /// <param name="_txtUnitPrice"></param>
        /// <param name="_txtUnitPriceExclVAT"></param>
        /// <param name="_priceIncludeVAT"></param>
        /// <param name="_taxgroupCode"></param>
        /// <param name="_discountGroup"></param>
        /// <param name="_salesProduct"></param>
        /// <param name="_active"></param>
        /// <param name="_purchaseProduct"></param>
        /// <param name="_returnAllowed"></param>
        /// <param name="_allowNegStock"></param>
        /// <param name="_createdBy"></param>
        public void UpdateProduct(string _txtNo, string _txtGTIN, string _txtProductName, string _txtDescription, string _categoryCode, string _unitOfMeasure, string _parentCode, string _brandID, decimal _costPrice, decimal _txtUnitPrice, decimal _txtUnitPriceExclVAT, char _priceIncludeVAT, string _taxgroupCode, string _discountGroup, char _salesProduct, char _active, char _purchaseProduct, char _returnAllowed, char _allowNegStock, string _createdBy)
        {
            string query = @"UPDATE [dbo].[INV_Product] SET [GTIN] = '" + _txtGTIN + "', [ProductName] = '" + _txtProductName + "', [Description] = '" + _txtDescription + "', [CategoryCode] = '" + _categoryCode + "', [UnitOfMeasureCode] = '" + _unitOfMeasure + "', [ParentProductCode] = '" + _parentCode + "', [BrandID] = '" + _brandID + "', [UnitCost] = '" + _costPrice + "', [UnitPrice] = '" + _txtUnitPrice + "', [UnitPriceExclVAT] = '" + _txtUnitPriceExclVAT + "', [PriceIncVAT] = '" + _priceIncludeVAT + "', [TaxGroupCode] = '" + _taxgroupCode + "', [DiscGroupCode] = '" + _discountGroup + "', [SalesProduct] = '" + _salesProduct + "', [Active] = '" + _active + "', [PurchaseProduct] = '" + _purchaseProduct + "', [ReturnAllowed] = '" + _returnAllowed + "', [AllowNegStock] = '" + _allowNegStock + "', [ModifiedBy] = '" + _createdBy + "', [ModifiedDate] = CONVERT(datetime,GETDATE(),103) WHERE [No] = '" + _txtNo + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update Purchase Invoice Header
        /// <summary>
        /// Update purchase Invoice Header
        /// </summary>
        /// <param name="_purchInvoiceNo"></param>
        /// <param name="_statusCode"></param>
        /// <param name="_subTotal"></param>
        /// <param name="_itemTax"></param>
        /// <param name="_tax1"></param>
        /// <param name="_tax2"></param>
        /// <param name="_tax3"></param>
        /// <param name="_itemDiscount"></param>
        /// <param name="_discountOnTotal"></param>
        /// <param name="_netPayable"></param>
        /// <param name="_taxID1"></param>
        /// <param name="_taxID2"></param>
        /// <param name="_taxID3"></param>
        /// <param name="_taxRate1"></param>
        /// <param name="_taxRate2"></param>
        /// <param name="_taxRate3"></param>
        /// <param name="_totalTaxable"></param>
        /// <param name="_modifiedByDeviceID"></param>
        /// <param name="_modifiedBy"></param>
        public void UpdatePurchaseInvoice(string _purchInvoiceNo, string _statusCode, decimal _subTotal, decimal _itemTax, decimal _tax1, decimal _tax2, decimal _tax3, decimal _itemDiscount, decimal _discountOnTotal, decimal _netPayable, string _taxID1, string _taxID2, string _taxID3, decimal _taxRate1, decimal _taxRate2, decimal _taxRate3, decimal _totalTaxable, string _modifiedByDeviceID, string _modifiedBy)
        {
            string query = @"UPDATE [dbo].[PUR_PurchaseInvoices] SET [StatusCode] = '" + _statusCode + "',[SubTotal] = '" + _subTotal + "',[ItemTax] = '" + _itemTax + "' ,[Tax1] = '" + _tax1 + "',[Tax2] = '" + _tax2 + "',[Tax3] = '" + _tax3 + "',[ItemDiscount] = '" + _itemDiscount + "',[DiscountOnTotal] = '" + _discountOnTotal + "',[NetPayable] = '" + _netPayable + "',[TaxID1] = '" + _taxID1 + "',[TaxID2] = '" + _taxID2 + "',[TaxID3] = '" + _taxID3 + "',[TaxRate1] = '" + _taxRate1 + "',[TaxRate2] = '" + _taxRate2 + "',[TaxRate3] = '" + _taxRate3 + "',[TotalTaxable] = '" + _totalTaxable + "',[ModifiedByDeviceID] = '" + _modifiedByDeviceID + "',[ModifiedBy] = '" + _modifiedBy + "',[ModifiedDate] = CONVERT(datetime,GETDATE(),103) WHERE [PurchaseInvoiceNo] = '" + _purchInvoiceNo + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        #region Update Sales Quote
        /// <summary>
        /// Update Sales Quote Status Code
        /// </summary>
        /// <param name="_quoteNo"></param>
        /// <param name="_statusCode"></param>
        /// <param name="_modifiedByDeviceID"></param>
        /// <param name="_modifiedBy"></param>
        public void UpdateSalesQuote(string _quoteNo, string _statusCode, string _modifiedByDeviceID, string _modifiedBy)
        {
            string query = @"UPDATE [dbo].[SAL_SalesQuotes] SET [StatusCode] = '" + _statusCode + "', [ModifiedBy] = '" + _modifiedBy + "', [ModifiedDate] = CONVERT(datetime,GETDATE(),103),[ModifiedByDeviceID] = '" + _modifiedByDeviceID + "' WHERE SalesQuoteNo = '" + _quoteNo + "'";
            using (SqlConnection cn = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                }
            }
        }
        #endregion

        //
        // Read
        //
        #region Get Company Info
        public DataTable GetCompanyInfo()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT *,(SELECT [BankName] FROM [dbo].[ACC_Bank] WHERE COMPID = C.COMPID) AS 'BankName',(SELECT [Branch] FROM [dbo].[ACC_Bank] WHERE COMPID = C.COMPID) AS 'Branch',(SELECT [AccountName] FROM [dbo].[ACC_Bank] WHERE COMPID = C.COMPID) AS 'AccountName',(SELECT [AccountNo] FROM [dbo].[ACC_Bank] WHERE COMPID = C.COMPID) AS 'AccountNo' FROM [dbo].[SYS_CompanyInfo] C";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get User
        //SELECT * FROM [dbo].[SYS_User]
        public DataTable GetUsers()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[SYS_User]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Devices
        public DataTable GetDevices()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[SYS_Device]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Printer Settings
        //int _deviceID, string _commName, int _timeOut, int _retryCount,int _baundRate, int _systemFlag
        public DataTable GetPrinterSettings()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[SYS_PrinterSettings]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Products
        /// <summary>
        /// Get Products
        /// </summary>
        /// <returns></returns>
        public DataTable GetProducts()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT *,(SELECT (SUM([QuantityIn])-SUM([QuantityOut])) FROM [dbo].[INV_InventoryStock] WHERE [ProductCode] = S.[No]) AS 'CurrentStock' FROM [dbo].[INV_Product] S";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        /// <summary>
        /// Get Products By Product Code, Category Code
        /// </summary>
        /// <param name="_code"></param>
        /// <returns></returns>
        public DataTable GetProducts(string _code)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT *,(SELECT [Description] FROM [dbo].[INV_ProductCategory] WHERE [CategoryCode] = S.[CategoryCode]) AS 'Category',(SELECT [Description] FROM [dbo].[INV_UnitOfMeasure] WHERE [UnitOfMeasureCode] = S.[UnitOfMeasureCode]) AS 'UnitOfMeasure',(SELECT (SUM([QuantityIn])-SUM([QuantityOut])) AS 'CurrentStock' FROM [dbo].[INV_InventoryStock] WHERE [ProductCode] = S.[No]) AS 'CurrentStock' FROM [dbo].[INV_Product] S WHERE [No] = '" + _code + "' OR [CategoryCode] ='" + _code + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetProductsByItemName(string _item)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT *,(SELECT [Description] FROM [dbo].[INV_ProductCategory] WHERE [CategoryCode] = S.[CategoryCode]) AS 'Category',(SELECT [Description] FROM [dbo].[INV_UnitOfMeasure] WHERE [UnitOfMeasureCode] = S.[UnitOfMeasureCode]) AS 'UnitOfMeasure',(SELECT (SUM([QuantityIn])-SUM([QuantityOut])) AS 'CurrentStock' FROM [dbo].[INV_InventoryStock] WHERE [ProductCode] = S.[No]) AS 'CurrentStock' FROM [dbo].[INV_Product] S WHERE [No] LIKE '%" + _item + "%' OR [ProductName] LIKE '%" + _item + "%'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        /// <summary>
        /// Get Products By Product Name, GTIN, No.
        /// </summary>
        /// <param name="_productName"></param>
        /// <param name="_gtin"></param>
        /// <param name="_no"></param>
        /// <returns></returns>
        public DataTable GetProducts(string _productName, string _no)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT *,(SELECT [Description] FROM [dbo].[INV_ProductCategory] WHERE [CategoryCode] = S.[CategoryCode]) AS 'Category',(SELECT (SUM([QuantityIn])-SUM([QuantityOut])) FROM [dbo].[INV_InventoryStock] WHERE [ProductCode] = S.[No]) AS 'CurrentStock' FROM [dbo].[INV_Product] S WHERE [No] = '" + _no + "' OR [ProductName]='" + _productName + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        public DataTable GetProductsByFilter(string _filter)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT *,(SELECT [Description] FROM [dbo].[INV_ProductCategory] WHERE [CategoryCode] = S.[CategoryCode]) AS 'Category',(SELECT (SUM([QuantityIn])-SUM([QuantityOut])) FROM [dbo].[INV_InventoryStock] WHERE [ProductCode] = S.[No]) AS 'CurrentStock' FROM [dbo].[INV_Product] S WHERE [No] = '" + _filter + "' OR  [GTIN] = '" + _filter + "' OR [ProductName]='" + _filter + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Categories
        /// <summary>
        /// Get all Product Categories
        /// </summary>
        /// <returns></returns>
        public DataTable GetCategories()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[INV_ProductCategory]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Customers
        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns></returns>
        public DataTable GetCustomers()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[CRM_Customers]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        /// <summary>
        /// Get Customer by Customer Code
        /// </summary>
        /// <param name="_code"></param>
        /// <returns></returns>
        public DataTable GetCustomers(string _code)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[CRM_Customers] WHERE [CustomerCode] = '" + _code + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Suppliers
        /// <summary>
        /// Get All Suppliers
        /// </summary>
        /// <returns></returns>
        public DataTable GetSuppliers()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[CRM_Suppliers]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Tax
        /// <summary>
        /// Get Tax Details
        /// </summary>
        /// <param name="_taxGroup"></param>
        /// <returns></returns>
        public DataTable GetTax(string _taxGroup)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[COM_TaxRate] WHERE [TaxGroupCode] = '" + _taxGroup + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Discount
        public DataTable GetDiscount(string _discountGroup)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[COM_DiscountGroup] WHERE [DiscountGroupCode] = '" + _discountGroup + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Pending Purchase Invoices
        /// <summary>
        /// Get Pending Purchase Invoices
        /// </summary>
        /// <param name="_PurchaseInvoiceNo"></param>
        /// <param name="_status"></param>
        /// <returns></returns>
        public DataTable GetPendingInvoices(string _PurchaseInvoiceNo, string _status)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = "SELECT * FROM [dbo].[PUR_PurchaseInvoices] WHERE [PurchaseInvoiceNo] = '" + _PurchaseInvoiceNo + "' OR [StatusCode] = '" + _status + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Sales Invoices
        /// <summary>
        /// Get Sales Invoice by Username and DeviceID
        /// </summary>
        /// <param name="_userName"></param>
        /// <param name="_deviceID"></param>
        /// <returns></returns>
        public DataTable GetSalesInvoices(string _userName, string _deviceID)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT TOP 3 [InvoiceDate],[SalesInvoiceNo],[StatusCode],[SubTotal]
                                    FROM [dbo].[SAL_SalesInvoices] WHERE StatusCode = 'Pending' AND CreatedBy = '" + _userName + "' OR CreatedByDeviceID = '" + _deviceID + "' ORDER BY InvoiceDate DESC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesInvoices(string invoiceNo)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT *
                                    FROM [dbo].[SAL_SalesInvoices] WHERE SalesInvoiceNo = '" + invoiceNo + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get SalesInvoiceLines
        public DataTable GetSalesInvoicesLines(DateTime startDate, DateTime endDate)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesInvoiceLines] where CreatedDate between '" + startDate + "' AND DATEADD(s,-1,DATEADD(d,1,'" + endDate + "')) ORDER BY [CreatedDate] DESC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesInvoicesLines()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesInvoiceLines] ORDER BY [CreatedDate] DESC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesInvoicesLines(string text, DateTime startDate, DateTime endDate)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesInvoiceLines] WHERE SalesInvoiceNo Like('%" + text + "%') OR ProductCode Like('%" + text + "%') OR Description Like('%" + text + "%') AND CreatedDate between '" + startDate + "' AND DATEADD(s,-1,DATEADD(d,1,'" + endDate + "')) ORDER BY [CreatedDate] DESC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesInvoicesLines_stringSearch(string text)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesInvoiceLines] WHERE SalesInvoiceNo Like('%" + text + "%') OR ProductCode Like('%" + text + "%') OR Description Like('%" + text + "%') ORDER BY [CreatedDate] DESC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesInvoicesLinesTotals(string _today)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM(LinePrice) AS 'SalesTotals' FROM [dbo].[SAL_SalesInvoiceLines] WHERE Convert(varchar,CreatedDate,101) = '" + _today + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        public DataTable GetSalesInvoicesLines(string _invoiceNo)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesInvoiceLines] WHERE SalesInvoiceNo = '" + _invoiceNo + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }


        #endregion

        #region Get Payment Modes
        /// <summary>
        /// Get All Fields in Payment Modes
        /// </summary>
        /// <returns></returns>
        public DataTable GetPaymentMode()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[ACC_PaymentModes] ORDER BY SeqNo ASC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Sales Quotes
        /// <summary>
        /// Get all SalesQuote Header
        /// </summary>
        /// <returns></returns>
        public DataTable GetSalesQuotes()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesQuotes] ORDER BY SalesQuoteNo";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }

        /// <summary>
        /// Get Sales Quote by Status Code
        /// </summary>
        /// <param name="_statusCode"></param>
        /// <returns></returns>
        public DataTable GetSalesQuotes(string _statusCode)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesQuotes] WHERE [StatusCode] = '" + _statusCode + "' ORDER BY [SalesQuoteNo] ASC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesQuotes_search(string text)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesQuotes] WHERE [SalesQuoteNo] LIKE '%" + text + "%' OR CustomerName LIKE '%"+text+"%' ORDER BY [SalesQuoteNo] ASC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        public DataTable GetSalesQuotes_search_dateFilter(string text, DateTime startDate, DateTime endDate)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[SAL_SalesQuotes] WHERE [SalesQuoteNo] LIKE '%" + text + "%' OR CustomerName LIKE '%" + text + "%' AND CreatedDate between '" + startDate + "' AND DATEADD(s,-1,DATEADD(d,1,'" + endDate + "')) ORDER BY [SalesQuoteNo] ASC";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Sales Quote Lines
        /// <summary>
        /// Get Sales Quote Lines by SalesQuoteNo
        /// </summary>
        /// <param name="_salesQuoteNo"></param>
        /// <returns></returns>
        public DataTable GetSalesQuoteLines(string _salesQuoteNo)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT *  FROM [dbo].[SAL_SalesQuoteLines] WHERE SalesQuoteNo = '" + _salesQuoteNo + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Card Details
        public DataTable GetCardDetails(int cardNo)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[ACC_PaymentByCard] WHERE [CardNo] = '" + cardNo + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get No. Series Entry Details
        public DataTable GetNoSeriesEntry()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT [Code], [Description], [DefaultNos], [ManualNos], [DateOrder]  FROM [dbo].[No_ Series]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get No. Series Lines
        public DataTable GetNoSeriesLines()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SeriesCode, ne.Description, StartingNo, EndingNo, LastNoUsed, isOpen, LastDateUsed, ne.DefaultNos, ne.ManualNos FROM [dbo].[No_ Series Line] nl JOIN [No_ Series] ne ON Code = nl.SeriesCode";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Cheque Details
        public DataTable GetChequeDetails(string _chequeNo)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT * FROM [dbo].[ACC_PaymentByCheque] WHERE [ChequeNo] = '" + _chequeNo + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion


        ///
        /// Z-Report
        ///
        #region Get CashIn
        public DataTable GetCashIn(string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM([TenderedAmount]) AS 'CashIn'  FROM [dbo].[ACC_PaymentLines] WHERE TransactionTypeCode != 'RETURN' AND TransactionDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get CashOut
        public DataTable GetCashOut(string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM([TenderedAmount]) AS 'CashOut'  FROM [dbo].[ACC_PaymentLines] WHERE TransactionTypeCode = 'RETURN' AND TransactionDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Distinct Users From Invoices
        public DataTable GetDistinctInvoiceUser(string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT DISTINCT CreatedBy FROM [dbo].[SAL_SalesInvoices] WHERE CreatedDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get PaymentModes
        public DataTable GetPaymentsModes()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT PaymentModeTypeCode FROM [dbo].[ACC_PaymentModes]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get PaymentModes Amounts by User and Date
        public DataTable GetPaymentsModesByUserDate(string user, string date, string tenderType)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM(TenderedAmount) AS 'Amount'
  FROM [dbo].[ACC_PaymentLines] WHERE TransactionTypeCode != 'RETURN' AND PaymentModeCode = '" + tenderType + "' AND CreatedBy ='" + user + "' AND TransactionDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get PaymentModes Amounts by Date
        public DataTable GetPaymentsModesByDate(string date, string tenderType)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM(TenderedAmount) AS 'Amount'
  FROM [dbo].[ACC_PaymentLines] WHERE TransactionTypeCode != 'RETURN' AND PaymentModeCode = '" + tenderType + "' AND TransactionDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Returns By TenderType
        public DataTable GetReturnsByTenderType(string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT DISTINCT [PaymentModeCode], SUM([TenderedAmount]) AS 'CashOut' FROM [dbo].[ACC_PaymentLines] WHERE [TransactionTypeCode] = 'RETURN' AND TransactionDate >= '" + date + "' GROUP BY [PaymentModeCode]";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Total Returns
        public DataTable GetTotalReturns(string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM([TenderedAmount]) AS 'TotalCashOut' FROM [dbo].[ACC_PaymentLines] WHERE [TransactionTypeCode] = 'RETURN' AND [TransactionDate] >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Tax Group
        public DataTable GetTaxGroup()
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT TaxGroupCode, [Tax], [TaxCategory] FROM [dbo].[COM_TaxRate] WHERE Tax > 0";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Tax Collected
        public DataTable GetTaxCollectedByTaxGroupDate(string code, string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM([LineTax1]) AS 'taxCollected'  FROM [dbo].[SAL_SalesInvoiceLines] WHERE LineTaxGroupID ='" + code + "' AND CreatedDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Total Taxable
        public DataTable GetTotalTaxable(string code, string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM(LinePrice) AS 'totalTaxable' FROM [dbo].[SAL_SalesInvoiceLines] WHERE LineTaxGroupID ='" + code + "' AND CreatedDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion

        #region Get Cummulative Tax
        public DataTable GetTotalTax(string code, string date)
        {
            var dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(connection))
            {
                string _query = @"SELECT SUM(LineTax1) AS 'cummulativeTax' FROM [dbo].[SAL_SalesInvoiceLines] WHERE LineTaxGroupID ='" + code + "' AND CreatedDate >= '" + date + "'";
                using (SqlCommand cmd = new SqlCommand(_query, cn))
                {
                    cn.Open();
                    dt.Load(cmd.ExecuteReader());
                }
            }
            return dt;
        }
        #endregion
    }
}
