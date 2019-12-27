export interface IUser {
    id: number;
    name: string;
    email: string;
}

export class User implements IUser {
    id: number;
    name: string;
    email: string;

    constructor(data?: IUser) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(data?: any) {
        if (data) {
            this.id = data["id"];
            this.name = data["name"];
            this.email = data["email"];
        }
    }

    static fromJS(data: any): User {
        let result = new User();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};

        data["id"] = this.id;
        data["name"] = this.name;
        data["email"] = this.email;

        return data;
    }
}