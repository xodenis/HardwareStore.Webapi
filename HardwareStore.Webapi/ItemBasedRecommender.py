import sys
import pandas as pd
import pyodbc
from sklearn.metrics.pairwise import cosine_similarity
from sklearn.metrics import mean_squared_error
from sklearn.model_selection import train_test_split
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import linear_kernel

# Define a connection string
con = pyodbc.connect(
    """DRIVER={ODBC Driver 17 for SQL Server};
    SERVER=(localdb)\\mssqllocaldb;
    DATABASE=HardwareStoreDB;
    Integrated Security=True""")
# Putting products data on 'products' dataframe
products = pd.read_sql(
    """SELECT p.Id, p.Name, p.Price, p.Characteristics, c.Name, sb.Name 
    FROM Products as p 
    INNER JOIN Categories as c 
    ON p.CategoryId=c.Id 
    INNER JOIN Subcategories as sb 
    ON p.SubcategoryId=sb.Id""", con)
products.columns = ['Id', 'Name', 'Price',
                    'Characteristics', 'Category', 'Subcategory']

# Function for merging features


def concatenate_features(df_row):
    return df_row['Characteristics']+';'+df_row['Category']+';'+df_row['Subcategory']


products['Features'] = products.apply(concatenate_features, axis=1)

tfidf = TfidfVectorizer()
# Construct the required TF-IDF matrix by applying the fit_transform
# method on the Characteristics feature
characteristics_matrix = tfidf.fit_transform(products['Features'])
# Find the similarity matrix using linear_kernel function
similarity_matrix = linear_kernel(
    characteristics_matrix, characteristics_matrix)
# Products index mapping
mapping = pd.Series(products.index, index=products['Id'])

# Recommender function that recommend products using cosine_similarity
def recommend_products(product_input):
    product_index = mapping[product_input]

    # Get similarity values with other products
    # similarity_score is the list of index and similarity matrix
    similarity_score = list(enumerate(similarity_matrix[product_index]))

    # Sort in descending order the similarity score of product inputted with all the other products
    similarity_score = sorted(
        similarity_score, key=lambda x: x[1], reverse=True)

    # Get the scores of the 5 most similar products. Ignore the first product.
    similarity_score = similarity_score[1:6]

    # Return product Id using the mapping series
    product_indices = [i[0] for i in similarity_score]
    return (products['Id'].iloc[product_indices])


print(recommend_products(int(sys.argv[1])))
