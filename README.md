# astore

API for a simple online store

### Authentication & user details
| Method | Path                      | Description                            | User authenticated | Admin only |
|--------|---------------------------|----------------------------------------|--------------------|------------|
| POST   | /auth/login               | jwt authorization by user and password |                    |            |
| POST   | /auth/register            | register user                          |                    |            |
| GET    | /users                    | get information about current user     | x                  |            |
| PUT    | /users                    | update information about current user  | x                  |            |
| GET    | /users/:id                | get information about user             | x                  | x          |

### Articles
| Method | Path          | Description       | User authenticated | Admin only |
|--------|---------------|-------------------|--------------------|------------|
| GET    | /articles     | get all articles  |                    |            |
| GET    | /articles/:id | get article by id |                    |            |
| POST   | /articles     | add an article    | x                  | x          |
| PUT    | /articles/:id | update an article | x                  | x          |
| DELETE | /articles/:id | delete an article | x                  | x          |

### Reviews
| Method | Path                      | Description             | User authenticated | Admin only |
|--------|---------------------------|-------------------------|--------------------|------------|
| GET    | /articles/:id/reviews     | get all article reviews |                    |            |
| GET    | /articles/:id/reviews/:id | get review details      |                    |            |
| POST   | /articles/:id/reviews     | post a review           | x                  |            |
| PUT    | /articles/:id/reviews/:id | update a review         | x                  |            |
| DELETE | /articles/:id/reviews/:id | delete a review         | x                  | if not own |

### Cart
| Method | Path                      | Description             | User authenticated | Admin only |
|--------|---------------------------|-------------------------|--------------------|------------|
| GET    | /cart                     | get current user's shopping cart       | x                  |            |
| POST   | /cart                     | checkout current user                  | x                  |            |
| PUT    | /cart                     | add an article to shopping cart        | x                  |            |
| DELETE | /cart                     | clear shopping cart                    | x                  |            |
| DELETE | /cart/:id                 | remove an article from shopping cart   | x                  |            |

### Wishlist
| Method | Path                      | Description             | User authenticated | Admin only |
|--------|---------------------------|-------------------------|--------------------|------------|
| GET    | /wishlist                 | get current user's wishlist            | x                  |            |
| POST   | /wishlist                 | move wishlist items to shopping cart   | x                  |            |
| PUT    | /wishlist                 | add articles to wishlist               | x                  |            |
| DELETE | /wishlist                 | remove all articles from wishlist      | x                  |            |
| DELETE | /wishlist/:id             | remove an article from wishlist        | x                  |            |

