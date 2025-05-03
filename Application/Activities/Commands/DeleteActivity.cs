using System;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public class Command : IRequest
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command>
    {
        public async Task Handle(Command request, CancellationToken cancellationToken)
        {
            // Find the activity in the database by its ID.
            // If the activity is not found, throw an exception.
            var activity = await context.Activities
                .FindAsync([request.Id], cancellationToken) 
                    ?? throw new Exception("Activity not found");

            // Remove the activity from the database context.
            context.Remove(activity);

            // Save the changes to the database asynchronously.
            // The cancellationToken allows the operation to be canceled if needed.
            await context.SaveChangesAsync(cancellationToken);
        }
    }

}
