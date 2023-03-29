using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyCashFlowControl.Application.Handlers
{
    public class AddTwoNumbersRequest : IRequest<string>
    {
        public int Num1 { get; init; }
        public int Num2 { get; init; }
    }

    // MediatR Request Handler
    public class AddTwoNumbersRequestHandler : IRequestHandler<AddTwoNumbersRequest, string>
    {
        public async Task<string> Handle(AddTwoNumbersRequest request, CancellationToken cancellationToken)
        {
            return $"You added {request.Num1} and {request.Num2}, the result is {request.Num1 + request.Num2}";
        }
    }
}
