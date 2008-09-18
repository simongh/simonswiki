using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Wiki_Config
{
    public class Configuration : ConfigurationSection
    {
        internal const string c_providers = "providers";
        internal const string c_Parser = "parser";
        
        [ConfigurationProperty(Configuration.c_providers)]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)this[c_providers]; }
            set { this[c_providers] = value; }
        }

        [ConfigurationProperty(Configuration.c_Parser)]
        public ParserConfig Parser
        {
            get { return (ParserConfig)this[c_Parser]; }
            set { this[c_Parser] = value; }
        }
    }
}
