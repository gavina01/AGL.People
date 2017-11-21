import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface PetType {
    gender: string,
    petNames: string[]
}

interface PetTypeSummary {
    items: PetType[]
}

interface SummaryState {
    summary: PetTypeSummary;
    loading: boolean;
}

export class Home extends React.Component<RouteComponentProps<{}>, SummaryState> {
    constructor() {
        super();
        this.state = { summary: { items: [] }, loading: true };

        fetch('api/Person/GetSummaryByPetType')
            .then(response => response.json() as Promise<PetTypeSummary>)
            .then(data => {
                this.setState({ summary: data, loading: false });
            });
    }

    public render() {
        let contents = this.state.loading
            ? <p><em>Loading</em></p>
            : Home.renderSummaryList(this.state.summary);
        return <div>
            {contents}
        </div>;
    }

    private static renderSummaryList(summaryToList: PetTypeSummary) {
        return <div className='divPetSummary'>
            {(summaryToList.items.length > 0) ? (
                <ul>
                    {summaryToList.items.map(summaryItem =>
                        <li key={summaryItem.gender}>
                            {summaryItem.gender}
                            <ul>
                                {summaryItem.petNames.map(pet =>
                                    <li key={summaryItem.gender + pet}>{pet}</li>
                                )}
                            </ul>
                        </li>
                    )}
                </ul>
            ) : (
                    <span>No Summary found</span>
                )
            }
        </div>;
    }
}




