namespace FutterlisteNg.Domain.Data

open FutterlisteNg.Domain.Model

type IUserRepository =
    abstract member All : seq<User>
    
type UserRepository() =
    interface IUserRepository with
        member this.All = Seq.empty
