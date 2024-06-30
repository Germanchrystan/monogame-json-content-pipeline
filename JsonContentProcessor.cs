using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using TInput = System.String;
using TOutput = JsonContentPipelineExtension.JsonContentProcessorResult;

namespace JsonContentPipelineExtension
{
    [ContentProcessor(DisplayName = "JSON Content Processor - GChrystan")]
    internal class JsonContentProcessor : ContentProcessor<TInput, TOutput>
    {
        [DisplayName("Minify Json")]
        public bool Minify { get; set; } = true;
        [DisplayName("Runtime Identifier")]
        public string RuntimeIdentifier { get; set; } = string.Empty;
        public override TOutput Process(TInput input, ContentProcessorContext context)
        {
            if(string.IsNullOrEmpty(RuntimeIdentifier))
            {
                throw new Exception("No Runtime Identifier was specifierd for this Json content");
            }
            if(Minify)
            {
                input = MinifyJson(input);
            }
            var result = new JsonContentProcessorResult();
            result.Json = input;
            result.RuntimeIdentifier = RuntimeIdentifier;
            return result;
        }

        private string MinifyJson(string input) 
        {
            JsonWriterOptions options = new JsonWriterOptions()
            {
                Indented = false,
            };

            JsonDocument doc = JsonDocument.Parse(input);

            using(MemoryStream stream = new MemoryStream())
            {
                using (Utf8JsonWriter write = new Utf8JsonWriter(stream))
                {
                    doc.WriteTo(write);
                    write.Flush();
                };
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
