import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import Resources from './components/Resources';

import './custom.css'

export default () => (
    <Layout>
        <Route exact path='/' component={Resources} />
        <Route path='/resources' component={Resources} />
    </Layout>
);
