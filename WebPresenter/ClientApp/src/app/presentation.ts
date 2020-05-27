export enum PresentationState {
  Slides,
  Text
}

export enum TextState {
  Html,
  Markdown,
  Monospace,
  Paragraphs
}

export interface Presentation {
  presentationState: PresentationState,
  textState: TextState,
  title: string,
  text: string,
  currentSlideNumber: number,
  numberOfSlides: number,
  imagePresentation: string[]
  permanentNotes: string,
  slideNotes: string[]
}
