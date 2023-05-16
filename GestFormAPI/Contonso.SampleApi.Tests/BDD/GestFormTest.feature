Feature: GestFormTest

A short summary of our gestForm test

Scenario: Testing an array of valid numbers
	Given the following list as an input
    | Array |
    | -15   |
    | 400   |
    | 16    |
    | 8     |
    | 999   |
    | 8     |

	When calling gestForm test API route 

	Then the following results will be sent as an output
    | Numbers | Results |
    | -15     | -15     |
    | 400     | 400     |
    | 16      | 16      |
    | 8       | 8       |
    | 999     | 999     |
    | 8       | 8       |

Scenario: Testing with an empty array
	Given an empty array as an input

	When calling gestForm test API route 

	Then an error with the following message will be thrown 'Please Could you insert a full valid array'

Scenario: Testing with an invalid array
	Given the following list as an input
    | Array |
    | -15   |
    | 400   |
    | 16    |
    | 8     |
    | -5555 |
    | 7777  |

	When calling gestForm test API route 

	Then an error with the following message will be thrown 'the following Array contains invalid numbers : [ -5555, 7777]'

