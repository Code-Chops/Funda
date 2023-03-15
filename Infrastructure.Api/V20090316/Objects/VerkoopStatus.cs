using System.Runtime.Serialization;

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public enum VerkoopStatus 
{ 
    StatusBeschikbaar,
    [EnumMember(Value = "Verkocht onder voorbehoud")]
    VerkochtOnderVoorbehoud,
    [EnumMember(Value = "Verkocht/verhuurd onder voorbehoud")]
    VerkochtVerhuurdOnderVoorbehoud,
    [EnumMember(Value = "Verhuurd onder voorbehoud")]
    VerhuurdOnderVoorbehoud,
    OnderBod,
    OnderOptie,
};