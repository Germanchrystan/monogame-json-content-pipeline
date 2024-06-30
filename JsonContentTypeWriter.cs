using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

using TInput = JsonContentPipelineExtension.JsonContentProcessorResult;

namespace JsonContentPipelineExtension
{
    [ContentTypeWriter]
    internal class JsonContentTypeWriter : ContentTypeWriter<TInput>
    {
        private string _runtimeIdentifier;
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return _runtimeIdentifier;
        }

        protected override void Write(ContentWriter output, TInput value)
        {
            output.Write(value.Json);
        }
    }
}
