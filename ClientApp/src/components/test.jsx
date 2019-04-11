import React, { Component } from 'react';

export default class Test extends Component {
  componentDidMount() {
    fetch("api/test/family", {
      method: 'GET',
      credentials: 'same-origin',
      headers: {
        'Accept': 'application/json, text/plain, */*',
        'Content-Type': 'application/json'
      }
    }).then(response => response.json()).then(data => {
      console.log(data);
    });
  }

  render() {
    return (
      <>Hello world!</>
    );
  }
}
