using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Article {

	public string id;
	public string name;
	public string period;
	public ArticleType articleType;

	public Article(){
	}

	public Article(string id, string name, string period, ArticleType articleType){
		this.id = id;
		this.name = name;
		this.period = period;
		this.articleType = articleType;
	}

	public override string ToString () {
		return "Article:\t id:" + id + "\t name:" + name + "\t period:" + period + "\t articleType:" + articleType;
	}
}

public enum ArticleType {
    Cloth,
    A2,
    A3,
}