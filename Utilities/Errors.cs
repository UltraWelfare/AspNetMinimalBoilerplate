using System.Diagnostics;
using FluentValidation.Results;
using OneOf;
using OneOf.Types;

namespace AspNetMinimalBoilerplate.Utilities;

public class Errors : OneOfBase<NotFound, GenericError, List<ValidationFailure>>
{
    protected Errors(OneOf<NotFound, GenericError, List<ValidationFailure>> input) : base(input)
    {
    }

    public bool IsNotFound => IsT0;
    public bool IsGenericError => IsT1;
    public bool IsValidationFailure => IsT2;

    public NotFound AsNotFound => AsT0;
    public GenericError AsGenericError => AsT1;
    public List<ValidationFailure> AsValidationFailure => AsT2;

    public static implicit operator Errors(NotFound input) => new(input);
    public static implicit operator Errors(GenericError input) => new(input);
    public static implicit operator Errors(List<ValidationFailure> input) => new(input);

    public IResult ToHttpResponse()
    {
        if (IsNotFound)
        {
            return TypedResults.NotFound();
        }

        if (IsGenericError)
        {
            return TypedResults.BadRequest(AsGenericError.Message);
        }

        if (IsValidationFailure)
        {
            return TypedResults.BadRequest(AsValidationFailure.GroupBy(fail => fail.PropertyName)
                .Select(group => new { PropertyName = group.Key, Errors = group.Select(fail => fail.ErrorMessage) }));
        }

        throw new UnreachableException();
    }
}