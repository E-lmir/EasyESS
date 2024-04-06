using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EasyESS.Services.EssSite
{
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    [Serializable, XmlRoot("configuration")]
    public partial class EssSiteWebConfig
    {
        public configurationLocation location { get; set; }
        [System.Xml.Serialization.XmlElement("system.webServer")]
        public configurationSystemwebServer systemwebServer { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocation
    {
        [System.Xml.Serialization.XmlElement("system.webServer")]
        public configurationLocationSystemwebServer systemwebServer { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string path { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public bool inheritInChildApplications { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocationSystemwebServer
    {
        public configurationLocationSystemwebServerHandlers handlers { get; set; }
        public configurationLocationSystemwebServerAspNetCore aspNetCore { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocationSystemwebServerHandlers
    {
        public configurationLocationSystemwebServerHandlersAdd add { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocationSystemwebServerHandlersAdd
    {
        [System.Xml.Serialization.XmlAttribute()]
        public string name { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string path { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string verb { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string modules { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string resourceType { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocationSystemwebServerAspNetCore
    {
        public configurationLocationSystemwebServerAspNetCoreEnvironmentVariables environmentVariables { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string processPath { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public bool stdoutLogEnabled { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string stdoutLogFile { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string hostingModel { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocationSystemwebServerAspNetCoreEnvironmentVariables
    {
        public configurationLocationSystemwebServerAspNetCoreEnvironmentVariablesEnvironmentVariable environmentVariable { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationLocationSystemwebServerAspNetCoreEnvironmentVariablesEnvironmentVariable
    {
        [System.Xml.Serialization.XmlAttribute()]
        public string name { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string value { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationSystemwebServer
    {
        public configurationSystemwebServerRewrite rewrite { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationSystemwebServerRewrite
    {
        [System.Xml.Serialization.XmlArrayItem("rule", IsNullable = false)]
        public configurationSystemwebServerRewriteRule[] rules { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationSystemwebServerRewriteRule
    {
        public configurationSystemwebServerRewriteRuleMatch match { get; set; }
        public configurationSystemwebServerRewriteRuleAction action { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string name { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public bool stopProcessing { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationSystemwebServerRewriteRuleMatch
    {
        [System.Xml.Serialization.XmlAttribute()]
        public string url { get; set; }
    }

    [Serializable()]
    [System.ComponentModel.DesignerCategory("code")]
    [System.Xml.Serialization.XmlType(AnonymousType = true)]
    public partial class configurationSystemwebServerRewriteRuleAction
    {
        [System.Xml.Serialization.XmlAttribute()]
        public string type { get; set; }

        [System.Xml.Serialization.XmlAttribute()]
        public string url { get; set; }
    }
}