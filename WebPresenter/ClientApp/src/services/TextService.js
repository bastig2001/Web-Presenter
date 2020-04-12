import {HubConnectionBuilder, HttpTransportType, LogLevel} from '@aspnet/signalr';

class TextWebsocketService {
    constructor() {
        this.connection = new HubConnectionBuilder()
            .withUrl('/text', {transport: HttpTransportType.WebSockets})
            .configureLogging(LogLevel.Information)
            .build();
        
        this.connection.start().catch(err => console.error(err, 'red'));
    }
}

const TextService = new TextWebsocketService();

export default TextService;
