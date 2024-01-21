using FluentValidation.Results;
using OneOf;
using OneOf.Types;

namespace AspNetMinimalBoilerplate.Utilities;

public class ValueOrError<T> : OneOfBase<T, Errors>
{
    protected ValueOrError(OneOf<T, Errors> input) : base(input)
    {
    }

    public bool IsOk => IsT0;
    public bool IsError => IsT1;

    public T AsOk => AsT0;
    public Errors AsError => AsT1;


    public static implicit operator ValueOrError<T>(T input) => new(input);
    public static implicit operator ValueOrError<T>(Errors input) => new(input);


    public static implicit operator ValueOrError<T>(NotFound input) => new((Errors)input);
    public static implicit operator ValueOrError<T>(GenericError input) => new((Errors)input);
    public static implicit operator ValueOrError<T>(List<ValidationFailure> input) => new((Errors)input);
}