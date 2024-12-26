Considerations:
There are 2 main ways to resolve this: one is searching each word in the matrix, which would work best if the matrix is small, and the second way, which consists in searching the entire word list, one row at a time, and then each column, which is more versatile, and would outperform the first approach, so this is the one I chose.

*Important*
I assumed is that the found words do not overlap, so the search restarts immediately after a match. (For example, if the line contains 'papapapa' and I'm searching for 'papa', it would only be found twice.)"
