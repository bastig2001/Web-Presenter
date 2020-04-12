import React from 'react';
import PresentationMenu from "./PresentationMenu";
import Presentation from "./Presentation";
import PresentationControls from "./PresentationControls";
import TextService from "../services/TextService";

export default class PresenterView extends React.Component {
    constructor(props) {
        super(props);
        this.state = {text: {__html: ""}};
        this.handleTextInput = this.handleTextInput.bind(this);
    }
    
    handleTextInput(text) {
        this.setState({text: text});
        TextService.connection.invoke('ChangeText', text);
    }
    
    render() {
        return (
            <section className="PresenterViw">
                <PresentationMenu/>
                <Presentation text={this.state.text}/>
                <PresentationControls callbackOnTextInput={this.handleTextInput}/>
            </section>
        );
    }
}
