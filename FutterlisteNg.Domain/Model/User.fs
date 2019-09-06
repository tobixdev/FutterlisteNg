namespace FutterlisteNg.Domain.Model

type User(Name:string, ShortName:string) =
    member this.Name = Name
    member this.ShortName = ShortName