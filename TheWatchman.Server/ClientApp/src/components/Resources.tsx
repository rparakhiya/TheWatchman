import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as ResourceStatusStore from '../store/ResourceStatuses';
import './Resources.css';

// At runtime, Redux will merge together...
type ResourcesProps =
  ResourceStatusStore.ResourceStatusesState // ... state we've requested from the Redux store
  & typeof ResourceStatusStore.actionCreators // ... plus action creators we've requested
  & RouteComponentProps<{ }>; // ... plus incoming routing parameters


class Resources extends React.PureComponent<ResourcesProps> {
  private interval: any;
  // This method is called when the component is first added to the document
  public componentDidMount() {
        this.ensureDataFetched();
        this.interval = setInterval(() => {
          this.ensureDataFetched();
      }, 30000);
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
  }

  public componentWillUnmount() {
      clearInterval(this.interval);
  }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">Resources</h1>
        {this.renderResourcesTable()}
        <br/>
        <p className="resources-page-footer">
            Last updated: {this.props.lastUpdatedOn ? this.props.lastUpdatedOn.toLocaleString() : 'fetching data...'}
        </p>
      </React.Fragment>
    );
  }

  private ensureDataFetched() {
    this.props.requestResourceStatuses();
  }

  private renderResourcesTable() {
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Name</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {this.props.resources.map((resource: ResourceStatusStore.ResourceStatus) =>
            <tr key={resource.resource.id}>
              <td title={`Recent Hearbeat: ${(resource.lastHeartbeat ? resource.lastHeartbeat.toLocaleString() : 'Never')}`}>
                <svg className={`bi bi-circle-fill ${this.getStatusColor(resource.status)}`} width="1em" height="1em" 
                    viewBox="0 0 16 16" fill="currentColor" xmlns="http://www.w3.org/2000/svg">
                    <circle cx="8" cy="8" r="8"/>
                </svg>&nbsp;&nbsp;{resource.resource.name}</td>
              <td>{resource.status}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  private getStatusColor(status: ResourceStatusStore.ResourceStatusTypes) {
      switch (status)
      {
          case ResourceStatusStore.ResourceStatusTypes.Online:
              return 'text-success';
          case ResourceStatusStore.ResourceStatusTypes.Offline:
              return 'text-danger';
          default:
              return 'text-warning';
      }
  }

}

export default connect(
  (state: ApplicationState) => state.resourceStatuses, // Selects which state properties are merged into the component's props
  ResourceStatusStore.actionCreators // Selects which action creators are merged into the component's props
)(Resources as any);
