import React from 'react';

export default class Presentation extends React.Component {
    render() {
        return (
            <div className="Presentation">
                <h1>Presentation</h1>
                <div dangerouslySetInnerHTML={this.props.text}/>
            </div>
        );
    }
}
