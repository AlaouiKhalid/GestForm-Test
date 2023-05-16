import { Injectable } from '@angular/core';

@Injectable()

//provides configurate to composer rest Server
export class Configuration {

    //localhost you have just to change to ip address
    public ApiIP: string = "https://localhost";    //localhost
    //port for the  Api
    public ApiPort: string = "51911";

    //this.ApiIP + ":" + this.ApiPort +link

    public GestFormLink: string =  "/GestForm";

}