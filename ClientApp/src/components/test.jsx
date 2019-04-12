import React, { Component } from 'react';

export default class Test extends Component {
  componentDidMount() {
    fetch("api/netpresentvalue", {
      method: 'POST',
      credentials: 'same-origin',
      headers: {
        'Accept': 'application/json, text/plain, */*',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        lowerBoundDiscount: 1,
        upperBoundDiscount: 1,
        discountIncrement: 0,
        amount: 1000,
        cashFlows: [
          { period: 1, amount: 500 },
          { period: 2, amount: 300 },
          { period: 3, amount: 800 }
        ]
      })
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
