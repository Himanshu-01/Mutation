/*
    Custom tab page class needs serious work. Need something to handle the opening of tags, and setting
    the streams endianness.
 
    All controls need to inherit a common base type so we can enumerate them with ease.
*/

/* ----------------- NOTES------------------

    Add sanity checks to the controls to make sure things like enum and bitmask values are not out of range.
 
    Change the color of the name of controls to red if there are problems with the data values associated with them.
 
    Create an option called "Advanced Mode" that will allow users to check or select any value in a bitmask/enum, etc.
 * 
*/