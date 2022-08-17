using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.ComponentModel;

namespace EnumTests
{
    public class Tests
    {
        [Test]
        public Task MapWithEnum()
        {
            var sut = new Option
            {
                OptionType = OptionType.FIRST,
                StringField = "test"
            };

            var settings = new VerifySettings();
            settings.AddExtraSettings(x => x.ContractResolver = new CustomResolver());

            return Verifier.Verify(sut, settings);
        }

        public class Option
        {
            private OptionType optionTypeField;
            private string stringField;
            public OptionType OptionType
            {
                get
                {
                    return this.optionTypeField;
                }
                set
                {
                    this.optionTypeField = value;
                }
            }

            public string StringField 
            { 
                get => stringField; 
                set => stringField = value; 
            }
        }

        public class CustomResolver : DefaultContractResolver
        {
            protected override JsonObjectContract CreateObjectContract(Type objectType)
            {
                JsonObjectContract contract = base.CreateObjectContract(objectType);
                if(objectType == typeof(Enum))
                {
                    contract.Converter = new StringEnumConverter();

                }
                return contract;
            }
        }
    }

    public enum OptionType
    {
        FIRST,
        SECOND
    }
}