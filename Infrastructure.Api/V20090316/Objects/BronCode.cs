using System.Runtime.Serialization;

namespace Fundalyzer.Infrastructure.Api.V20090316.Objects;

[SuppressMessage("ReSharper", "IdentifierTypo")]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public enum BronCode 
{
    [EnumMember(Value = "LMV")]
    Lmv,
    [EnumMember(Value = "MZV")]
    Mzv,
    [EnumMember(Value = "NVM")]
    Nvm,
    [EnumMember(Value = "VBO")]
    Vbo,
    [EnumMember(Value = "VGM")]
    Vgm,
};