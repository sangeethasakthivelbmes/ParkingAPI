﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Parking.WebAPI.Resources.UserService {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Common {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Common() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Parking.WebAPI.Resources.UserService.Common", typeof(Common).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {fieldName} is required.
        /// </summary>
        internal static string US_COMMON_001 {
            get {
                return ResourceManager.GetString("US_COMMON_001", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {fieldName} must be between {minLength} and {maxLength} characters long.
        /// </summary>
        internal static string US_COMMON_002 {
            get {
                return ResourceManager.GetString("US_COMMON_002", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {fieldName} should be digit and it should be {length} digit.
        /// </summary>
        internal static string US_COMMON_003 {
            get {
                return ResourceManager.GetString("US_COMMON_003", resourceCulture);
            }
        }

        /// <summary>
        ///   Looks up a localized string similar to {fieldName} invalid.
        /// </summary>
        internal static string US_COMMON_004
        {
            get
            {
                return ResourceManager.GetString("US_COMMON_004", resourceCulture);
            }
        }
    }
}
