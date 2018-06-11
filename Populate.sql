delete from Hangman..Categories;
delete from Hangman..Words;
delete from Hangman..Games;

insert into Hangman..Categories (CategoryID, CategoryName) values (1, 'States');
insert into Hangman..Categories (CategoryID, CategoryName) values (2, 'Cities');


insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Washington', 'State in the north west');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Oregon', 'State in the north west');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'California', 'State in the west');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Nevada', 'State in the west');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Arizona', 'State in the south');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Utah', 'State in the south');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Idaho', 'State in the north west');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Montana', 'State in the north west');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Wyoming', 'State in the midwest');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Colorado', 'State in the midwest');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Pennsylvania', 'State in the east');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (1, 'Tennessee', 'State in the east');


insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Seattle', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Bellevue', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Redmond', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Kirkland', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Bothel', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Spokane', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Vancouver', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Renton', 'City in Washington state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Irvine', 'City in California state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Dalas', 'City in California state');
insert into Hangman..Words (CategoryID, WordText, WordDescription) values (2, 'Bostun', 'City in Massachusetts');


