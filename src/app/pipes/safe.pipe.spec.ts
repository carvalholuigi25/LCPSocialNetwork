import { TestBed } from '@angular/core/testing';
import { DomSanitizer } from '@angular/platform-browser';
import { SafePipe } from './safe.pipe';

describe('SafePipe', () => {
  let pipe: SafePipe;
  let sanitizer: DomSanitizer;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    sanitizer = TestBed.inject(DomSanitizer);
    pipe = new SafePipe(sanitizer);
  });

  it('should create an instance', () => {
    expect(pipe).toBeTruthy();
  });

  it('should transform HTML value', () => {
    const htmlValue = '<p>Hello, <strong>world</strong></p>';
    const transformedValue = pipe.transform(htmlValue, 'html');
    expect(transformedValue).toBeTruthy();
    // Add more specific assertions here
  });

  it('should transform style value', () => {
    const styleValue = 'color: red;';
    const transformedValue = pipe.transform(styleValue, 'style');
    expect(transformedValue).toBeTruthy();
    // Add more specific assertions here
  });

  it('should transform script value', () => {
    const scriptValue = 'console.log("Hello, world!");';
    const transformedValue = pipe.transform(scriptValue, 'script');
    expect(transformedValue).toBeTruthy();
    // Add more specific assertions here
  });

  it('should transform URL value', () => {
    const urlValue = '[1](https://example.com)';
    const transformedValue = pipe.transform(urlValue, 'url');
    expect(transformedValue).toBeTruthy();
    // Add more specific assertions here
  });

  it('should transform resource URL value', () => {
    const resourceUrlValue = '[2](https://example.com/image.jpg)';
    const transformedValue = pipe.transform(resourceUrlValue, 'resourceUrl');
    expect(transformedValue).toBeTruthy();
    // Add more specific assertions here
  });

  it('should transform markdown value', () => {
    const markdownValue = '# Heading';
    const transformedValue = pipe.transform(markdownValue, 'markdown');
    expect(transformedValue).toBeTruthy();
    // Add more specific assertions here
  });

  it('should throw an error for invalid safe type', () => {
    const invalidType = 'invalid';
    expect(() => pipe.transform('value', invalidType)).toThrowError(`Invalid safe type specified: ${invalidType}`);
  });
});