import { Action, Reducer } from 'redux';
import { AppThunkAction } from './';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface ResourceStatusesState {
    isLoading: boolean;
    resources: ResourceStatus[];
    lastUpdatedOn?: Date;
}

export enum ResourceStatusTypes {
    Online = "Online",
    Offline = "Offline",
    Degraded = "Degraded",
    Maintenance = "Maintenance"
}

export interface ResourceStatus {
    resource: {
        id: string,
        name: string,
        description: string
    },
    status: ResourceStatusTypes,
    lastHeartbeat: Date
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestResourceStatusesAction {
    type: 'REQUEST_RESOURCE_STATUSES';
}

interface ReceiveResourceStatusesAction {
    type: 'RECEIVE_RESOURCE_STATUSES';
    resources: ResourceStatus[];
}

// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestResourceStatusesAction | ReceiveResourceStatusesAction;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestResourceStatuses: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.resourceStatuses) {
            fetch(`api/resources/status`)
                .then(response => response.json() as Promise<ResourceStatus[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_RESOURCE_STATUSES', resources: data });
                });

            dispatch({ type: 'REQUEST_RESOURCE_STATUSES' });
        }
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: ResourceStatusesState = { resources: [], isLoading: false, lastUpdatedOn: undefined };

export const reducer: Reducer<ResourceStatusesState> = (state: ResourceStatusesState | undefined, incomingAction: Action): ResourceStatusesState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_RESOURCE_STATUSES':
            return {
                resources: state.resources,
                isLoading: true,
                lastUpdatedOn: state.lastUpdatedOn
            };
            break;
        case 'RECEIVE_RESOURCE_STATUSES':
            return {
                resources: action.resources,
                isLoading: false,
                lastUpdatedOn: new Date()
            };
            break;
    }

    return state;
};
