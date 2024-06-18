from trafilatura import fetch_url, html2txt
from trafilatura.sitemaps import sitemap_search
import sys

def retrieve_website(url: str):
    downloaded = fetch_url(url)
    text = html2txt(downloaded)
    return text

# TODO: test if sitemap_search is enough because web crawling is very slow
def search(homepage: str):
    print(homepage)
    links = sitemap_search(homepage, target_lang="en")
    print(links)
    text = retrieve_website(homepage) + " "
    for link in links:
        text += retrieve_website(link)
    return text

if __name__ == "__main__":
    example_homepage = sys.argv[1]
    text = search(example_homepage)
    print(text)