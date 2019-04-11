import React, { Component } from 'react';
import { Switch, Route } from 'react-router-dom';
import Test from './test';

class Main extends Component {
    render() {
      return (
        <main>
            <Switch>
                <Route exact path='/' component={Test}/>
            </Switch>
        </main>
      );
    }
}
  

export default Main
