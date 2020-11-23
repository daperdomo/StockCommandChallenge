import React, { Component } from 'react';

export class MessageList extends Component {

    render() {
        return (
            <ul style={{
                listStyle: "none",
                width: "100%",
                height: 300,
                overflowY: "scroll",
                padding: "0",
                border: "1px solid black"
            }} id="message-list">
                {this.props.loading ?
                    <li >
                        <strong>Loading</strong>
                    </li>
                    :
                    this.props.messages.map(function (item, index) {
                        var d = new Date(item.date);
                        return (<li key={index}>
                            <strong>{item.userName} ({formatDate(d)}):</strong> {item.text}
                        </li>);
                    })}
            </ul>
        );
    }
}

function formatDate(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var ampm = hours >= 12 ? 'pm' : 'am';
    hours = hours % 12;
    hours = hours ? hours : 12; // the hour '0' should be '12'
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return (date.getMonth() + 1) + "/" + date.getDate() + "/" + date.getFullYear() + " " + strTime;
}