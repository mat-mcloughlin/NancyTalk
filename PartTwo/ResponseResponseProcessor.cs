namespace PartTwo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Nancy;
    using Nancy.Responses.Negotiation;

    public class ResponseResponseProcessor : IResponseProcessor
    {
        public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
        {
            get
            {
                return Enumerable.Empty<Tuple<string, MediaRange>>();
            }
        }

        public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return new ProcessorMatch
                       {
                           ModelResult = (model is Response) ? MatchResult.ExactMatch : MatchResult.NoMatch,
                           RequestedContentTypeResult = (requestedMediaRange == "text/html") ? MatchResult.ExactMatch : MatchResult.NoMatch
                       };
        }

        public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
        {
            return (Response)model;
        }
    }
}
