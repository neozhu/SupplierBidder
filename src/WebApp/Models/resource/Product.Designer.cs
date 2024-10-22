namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class Product {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Product() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.Product", typeof(Product).Assembly);
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
    public static string Name {
            get {
                return ResourceManager.GetString("Name", resourceCulture);
            }
    }
    public static string Unit {
            get {
                return ResourceManager.GetString("Unit", resourceCulture);
            }
    }
    public static string UnitPrice {
            get {
                return ResourceManager.GetString("UnitPrice", resourceCulture);
            }
    }
    public static string StockQty {
            get {
                return ResourceManager.GetString("StockQty", resourceCulture);
            }
    }
    public static string IsRequiredQc {
            get {
                return ResourceManager.GetString("IsRequiredQc", resourceCulture);
            }
    }
    public static string ConfirmDateTime {
            get {
                return ResourceManager.GetString("ConfirmDateTime", resourceCulture);
            }
    }
    public static string CategoryId {
            get {
                return ResourceManager.GetString("CategoryId", resourceCulture);
            }
    }
 }
}