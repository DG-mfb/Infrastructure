﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Federation.Protocols.Request.Elements
{
    /// <summary>
    /// The <c>&lt;EncryptedAssertion&gt;</c> element represents an assertion in encrypted fashion, as defined by the
    /// XML Encryption Syntax and Processing specification [XMLEnc].
    /// </summary>
    [Serializable]
    [XmlType(Namespace = Saml20Constants.Assertion)]
    [XmlRoot(ElementName, Namespace = Saml20Constants.Assertion, IsNullable = false)]
    public class EncryptedAssertion : EncryptedElement
    {
        /// <summary>
        /// The XML Element name of this class
        /// </summary>
        public new const string ElementName = "EncryptedAssertion";
    }
}
