namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class ShippingOrder {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ShippingOrder() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.ShippingOrder", typeof(ShippingOrder).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
    public static string Id {
            get {
                return ResourceManager.GetString("Id", resourceCulture);
            }
    }
    public static string SO {
            get {
                return ResourceManager.GetString("SO", resourceCulture);
            }
    }
    public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
    }
    public static string ShippingDate {
            get {
                return ResourceManager.GetString("ShippingDate", resourceCulture);
            }
    }
    public static string DeliveryAddress {
            get {
                return ResourceManager.GetString("DeliveryAddress", resourceCulture);
            }
    }
    public static string Buyer {
            get {
                return ResourceManager.GetString("Buyer", resourceCulture);
            }
    }
    public static string BuyerPhone {
            get {
                return ResourceManager.GetString("BuyerPhone", resourceCulture);
            }
    }
    public static string Packages {
            get {
                return ResourceManager.GetString("Packages", resourceCulture);
            }
    }
    public static string TotalAmount {
            get {
                return ResourceManager.GetString("TotalAmount", resourceCulture);
            }
    }
    public static string InvoiceNo {
            get {
                return ResourceManager.GetString("InvoiceNo", resourceCulture);
            }
    }
    public static string InvoiceAmount {
            get {
                return ResourceManager.GetString("InvoiceAmount", resourceCulture);
            }
    }
    public static string TaxRate {
            get {
                return ResourceManager.GetString("TaxRate", resourceCulture);
            }
    }
    public static string Tax {
            get {
                return ResourceManager.GetString("Tax", resourceCulture);
            }
    }
    public static string Remark {
            get {
                return ResourceManager.GetString("Remark", resourceCulture);
            }
    }
    public static string ReceivedDate {
            get {
                return ResourceManager.GetString("ReceivedDate", resourceCulture);
            }
    }
    public static string ClosedDate {
            get {
                return ResourceManager.GetString("ClosedDate", resourceCulture);
            }
    }
    public static string UserName {
            get {
                return ResourceManager.GetString("UserName", resourceCulture);
            }
    }
    public static string SupplierId {
            get {
                return ResourceManager.GetString("SupplierId", resourceCulture);
            }
    }
    public static string SupplierName {
            get {
                return ResourceManager.GetString("SupplierName", resourceCulture);
            }
    }
 }
}