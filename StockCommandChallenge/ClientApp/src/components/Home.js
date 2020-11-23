import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hello!</h1>
        <p>Welcome to financial chat bot:</p>
        <p>To help you get started, go to Stock Chat section and for more imformation type /help</p>
      </div>
    );
  }
}
