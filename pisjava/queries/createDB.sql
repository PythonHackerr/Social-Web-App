CREATE TABLE authority(
	id INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    name VARCHAR(30) NOT NULL UNIQUE,
    PRIMARY KEY(id)
);

CREATE TABLE category(
    id INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL UNIQUE,
    PRIMARY KEY(id)
);

CREATE TABLE user(
	id INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL UNIQUE,
    password VARCHAR(100) NOT NULL,
    email VARCHAR(50) NOT NULL UNIQUE,
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    authority_id SMALLINT UNSIGNED DEFAULT NULL,
    PRIMARY KEY(id),
    FOREIGN KEY(authority_id) REFERENCES authority(id) ON DELETE SET NULL
);

CREATE TABLE post(
	id INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    header VARCHAR(255) NOT NULL,
    content VARCHAR(2047) NOT NULL,
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    creator_id INT UNSIGNED NOT NULL,
    category_id INT UNSIGNED NOT NULL,
	PRIMARY KEY (id),
    FOREIGN KEY (category_id) REFERENCES category(id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES user(id) ON DELETE CASCADE
);

CREATE TABLE post_thumbs_up(
	user_id INT UNSIGNED NOT NULL,
	post_id INT UNSIGNED NOT NULL,
	FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE,
	FOREIGN KEY (post_id) REFERENCES post(id) ON DELETE CASCADE
);

CREATE TABLE post_thumbs_down(
	user_id INT UNSIGNED NOT NULL,
	post_id INT UNSIGNED NOT NULL,
	FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE,
	FOREIGN KEY (post_id) REFERENCES post(id) ON DELETE CASCADE
);

CREATE TABLE comment(
    id INT UNSIGNED NOT NULL UNIQUE AUTO_INCREMENT,
    message VARCHAR(2000) NOT NULL,
    create_time TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    post_id INT UNSIGNED NOT NULL,
    creator_id INT UNSIGNED NOT NULL,
    PRIMARY KEY (id),
    FOREIGN KEY (post_id) REFERENCES post(id) ON DELETE CASCADE,
    FOREIGN KEY (creator_id) REFERENCES user(id) ON DELETE CASCADE
);

CREATE TABLE comment_thumbs_up(
    user_id INT UNSIGNED NOT NULL,
    comment_id INT UNSIGNED NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE,
    FOREIGN KEY (comment_id) REFERENCES comment(id) ON DELETE CASCADE
);

CREATE TABLE comment_thumbs_down(
    user_id INT UNSIGNED NOT NULL,
    comment_id INT UNSIGNED NOT NULL,
    FOREIGN KEY (user_id) REFERENCES user(id) ON DELETE CASCADE,
    FOREIGN KEY (comment_id) REFERENCES comment(id) ON DELETE CASCADE
);