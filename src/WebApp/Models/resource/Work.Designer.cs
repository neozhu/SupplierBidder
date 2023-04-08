namespace WebApp.Models.resource {
  using System;
  [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
  [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
  public class Work {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Work() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("WebApp.Models.resource.Work", typeof(Work).Assembly);
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
    public static string Status {
            get {
                return ResourceManager.GetString("Status", resourceCulture);
            }
    }
    public static string Assignee {
            get {
                return ResourceManager.GetString("Assignee", resourceCulture);
            }
    }
    public static string StartDate {
            get {
                return ResourceManager.GetString("StartDate", resourceCulture);
            }
    }
    public static string EndDate {
            get {
                return ResourceManager.GetString("EndDate", resourceCulture);
            }
    }
    public static string ToDoDateTime {
            get {
                return ResourceManager.GetString("ToDoDateTime", resourceCulture);
            }
    }
    public static string Enableed {
            get {
                return ResourceManager.GetString("Enableed", resourceCulture);
            }
    }
    public static string Completed {
            get {
                return ResourceManager.GetString("Completed", resourceCulture);
            }
    }
    public static string Hour {
            get {
                return ResourceManager.GetString("Hour", resourceCulture);
            }
    }
    public static string Priority {
            get {
                return ResourceManager.GetString("Priority", resourceCulture);
            }
    }
    public static string Score {
            get {
                return ResourceManager.GetString("Score", resourceCulture);
            }
    }
 }
}