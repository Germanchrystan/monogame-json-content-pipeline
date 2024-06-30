using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.IO;
using System.Text.Json;
using TImport = System.String;

namespace JsonContentPipelineExtension
{
    [ContentImporter(".json", DisplayName = "Json Importer - Gchrystan", DefaultProcessor = nameof(JsonContentProcessor))]
    public class JsonContentImporter : ContentImporter<TImport>
    {
        public override TImport Import(string filename, ContentImporterContext context)
        {
            string json = File.ReadAllText(filename);
            ValidateJson(json);
            return json;

        }

        private void ValidateJson(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                throw new InvalidContentException("The JSON file is empty");
            }
            try
            {
                _ = JsonDocument.Parse(json);
            }
            catch(Exception ex)
            {
                throw new InvalidContentException($"This does not appear to be a valid JSON file. {ex.Message}");
            }
        }
    }
}
