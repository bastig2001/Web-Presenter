import React from 'react';
import { Remarkable } from 'remarkable';

export default class PresentationControls extends React.Component {
    constructor(props) {
        super(props);
        this.handleTextInput = this.handleTextInput.bind(this);
        this.md = new Remarkable();
    }
    
    handleTextInput(e) {
        this.props.callbackOnTextInput({__html: this.md.render(e.target.value)});
    }
    
    render() {
        return (
            <div className="PresentationControls">
                <h1>Controls</h1>
                <textarea onChange={this.handleTextInput}/>
            </div>
        );
    }
}
