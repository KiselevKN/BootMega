﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Service.HexFile.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Service.HexFile.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Address 0x{0:X} out of range [{1}, 0x{2:X}].
        /// </summary>
        internal static string AddressOutOfRange {
            get {
                return ResourceManager.GetString("AddressOutOfRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Format &quot;{0}&quot; not supported.
        /// </summary>
        internal static string FormatNotSupported {
            get {
                return ResourceManager.GetString("FormatNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Incorrect comparison of memory areas.
        /// </summary>
        internal static string IncorrectComparisionOfMemoryAreas {
            get {
                return ResourceManager.GetString("IncorrectComparisionOfMemoryAreas", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid memory size.
        /// </summary>
        internal static string InvalidMemorySize {
            get {
                return ResourceManager.GetString("InvalidMemorySize", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid record checksum {0}.
        /// </summary>
        internal static string InvalidRecordChecksum {
            get {
                return ResourceManager.GetString("InvalidRecordChecksum", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid record length {0}.
        /// </summary>
        internal static string InvalidRecordLength {
            get {
                return ResourceManager.GetString("InvalidRecordLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid record with address.
        /// </summary>
        internal static string InvalidRecordWithAddress {
            get {
                return ResourceManager.GetString("InvalidRecordWithAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid record with data.
        /// </summary>
        internal static string InvalidRecordWithData {
            get {
                return ResourceManager.GetString("InvalidRecordWithData", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The record {0} does not match the pattern {1}.
        /// </summary>
        internal static string RecordDoesNotMatchThePattern {
            get {
                return ResourceManager.GetString("RecordDoesNotMatchThePattern", resourceCulture);
            }
        }
    }
}
