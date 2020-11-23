import React, { Component } from 'react';
import { MessageList } from './MessageList';
import authService from './api-authorization/AuthorizeService'
import { HubConnectionBuilder } from '@aspnet/signalr';

export class StockChat extends Component {
    static displayName = StockChat.name;

    constructor(props) {
        super(props);
        this.state = { messages: [], currentMessage: '', loading: true, hubConnection: null };

        this.onKeyUpMessage = this.onKeyUpMessage.bind(this);
        this.onChangeMessage = this.onChangeMessage.bind(this);
    }

    componentDidMount() {
        let hubConnection = new HubConnectionBuilder()
            .withUrl("/chatHub")
            .build();

        console.log(hubConnection);

        hubConnection.on('SendMessage', (userName, receivedMessage, date) => {
            let { messages } = this.state;
            var msg = {
                text: receivedMessage,
                userName,
                date
            }
            messages.push(msg);
            messages = messages.reverse().slice(0, 50).reverse();
            this.setState({
                messages: messages
            }, this.ScrollToBottom);
        });

        hubConnection.on('Initialize', (messages) => {
            var messagesObj = messages.reverse().map(item => {
                return {
                    text: item.messageText,
                    userName: item.username,
                    date: item.created
                }
            });
            this.setState({
                messages: messagesObj,
                loading: false
            }, this.ScrollToBottom);
        });

        this.setState({ hubConnection }, () => {
            this.state.hubConnection
                .start()
                .then(() => {
                    this.Initialize();
                    console.log('Connection started!');
                })
                .catch(err => console.log('Error while establishing connection', err));

        });
    }

    Initialize() {
        authService.getUser().then(user => {
            this.state.hubConnection
                .invoke('Initialize', user.name)
                .catch(err => console.error(err));
        });
    }

    ScrollToBottom() {
        var objDiv = document.getElementById("message-list");
        objDiv.scrollTop = objDiv.scrollHeight;
    }

    async onKeyUpMessage(e) {
        let { currentMessage } = this.state;
        if (e.keyCode === 13 && currentMessage && currentMessage.trim() !== '') {
            const user = await authService.getUser();

            this.state.hubConnection
                .invoke('SendMessage', user.name, currentMessage)
                .catch(err => console.error(err));
            this.setState({
                currentMessage: ''
            }, this.ScrollToBottom);
        };
    }

    onChangeMessage(e) {
        this.setState({ currentMessage: e.target.value });
    }

    render() {
        const { messages, currentMessage, loading } = this.state;
        return (
            <div>
                <MessageList messages={messages} loading={loading} />
                <input style={{
                    width: "100%"
                }} type="text" onKeyUp={this.onKeyUpMessage} onChange={this.onChangeMessage} disabled={loading} value={currentMessage} />
            </div>
        );
    }

    async LoadUser() {
        const user = await authService.getUser();
    }
}