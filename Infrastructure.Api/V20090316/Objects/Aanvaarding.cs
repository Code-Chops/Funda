using System.Runtime.Serialization;

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public enum Aanvaarding 
{ 
    BeschikbaarPerDirect, 
    InOverleg, 
    [EnumMember(Value = "BeschikbaarPer [BeschikbaarheidsDatum]")]
    BeschikbaarPerDatum,
    NoRiskClausuleMogelijk,
};